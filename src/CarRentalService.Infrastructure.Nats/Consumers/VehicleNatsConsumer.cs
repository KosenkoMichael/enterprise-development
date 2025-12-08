using CarRentalService.Core.Contracts.Dto;
using CarRentalService.Core.Contracts.Mappers;
using CarRentalService.Core.Domain.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Net;

namespace CarRentalService.Infrastructure.Nats.Consumers;

/// <summary>
/// Nats consumer for vehicles
/// </summary>
/// <param name="connection">nats connection</param>
/// <param name="scopeFactory">service scope factory</param>
/// <param name="configuration">configuration</param>
public class VehicleNatsConsumer(INatsConnection connection, IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<VehicleNatsConsumer> logger) : BackgroundService
{
    private readonly string _streamName = configuration.GetSection("Nats")["StreamName"] ?? throw new KeyNotFoundException("StreamName section of Nats is missing");
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
                    FilterSubjects = ["car-rental.vehicles.*"],
                    AckPolicy = ConsumerConfigAckPolicy.Explicit
                },
                stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var message in consumer.ConsumeAsync<List<VehicleRequest>>(cancellationToken: stoppingToken))
                {
                    if (message.Data is null) continue;
                    using var scope = scopeFactory.CreateScope();
                    var vehicleService = scope.ServiceProvider.GetRequiredService<IVehicleService>();
                    foreach (var vehicleRequest in message.Data)
                        await vehicleService.CreateVehicleAsync(vehicleRequest.ToDomain());
                    await message.AckAsync();
                    logger.LogInformation("VehicleRequest is resieved");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error when recieving VehicleRequest");
        }

    }
}