using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Net;

public class NatsGenericConsumer<TMessage, TService>(
    INatsConnection connection,
    IServiceScopeFactory scopeFactory,
    string subjectPattern,
    Func<TService, TMessage, Task> messageHandler,
    ILogger<NatsGenericConsumer<TMessage, TService>> logger,
    string streamName
) : BackgroundService where TService : notnull
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await connection.ConnectAsync();
            var context = connection.CreateJetStreamContext();
            var consumer = await context.CreateConsumerAsync(streamName,
                new ConsumerConfig
                {
                    FilterSubjects = new[] { subjectPattern },
                    AckPolicy = ConsumerConfigAckPolicy.Explicit
                },
                stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var message in consumer.ConsumeAsync<List<TMessage>>(cancellationToken: stoppingToken))
                {
                    if (message.Data is null) continue;

                    using var scope = scopeFactory.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<TService>();

                    foreach (var item in message.Data)
                    {
                        await messageHandler(service, item);
                    }

                    await message.AckAsync();
                    logger.LogInformation($"{typeof(TMessage).Name} received and processed.");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error when receiving {typeof(TMessage).Name}");
        }
    }
}
