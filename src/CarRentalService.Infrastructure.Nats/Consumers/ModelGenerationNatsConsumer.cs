using CarRentalService.Core.Contracts.Dto;
using CarRentalService.Core.Contracts.Mappers;
using CarRentalService.Core.Domain.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Net;

namespace CarRentalService.Infrastructure.Nats.Consumers;
public class ModelGenerationNatsConsumer(INatsConnection connection, IServiceScopeFactory scopeFactory, IConfiguration configuration) : BackgroundService
{
    private readonly string _streamName = configuration.GetSection("Nats")["StreamName"] ?? throw new KeyNotFoundException("StreamName section of Nats is missing");
    private readonly string _subjectName = configuration.GetSection("Nats")["SubjectName"] ?? throw new KeyNotFoundException("SubjectName section of Nats is missing");
    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await connection.ConnectAsync();
            var context = connection.CreateJetStreamContext();
            var consumer = await context.CreateConsumerAsync(_streamName,
                new ConsumerConfig
                {
                    FilterSubjects = ["car-rental.modelgenerations.*"],
                    AckPolicy = ConsumerConfigAckPolicy.Explicit
                },
                stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var message in consumer.ConsumeAsync<List<ModelGenerationRequest>>(cancellationToken: stoppingToken))
                {
                    if (message.Data is null) continue;
                    using var scope = scopeFactory.CreateScope();
                    var modelGenerationService = scope.ServiceProvider.GetRequiredService<IModelGenerationService>();
                    foreach (var modelGenerationRequest in message.Data)
                        await modelGenerationService.CreateModelGenerationAsync(modelGenerationRequest.ToDomain());
                    await message.AckAsync();
                }
            }
        }
        catch (Exception ex)
        {

        }

    }
}