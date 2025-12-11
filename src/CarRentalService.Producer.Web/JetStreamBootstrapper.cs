using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;

/// <summary>
/// JetStream bootstrapper for NATS
/// </summary>
/// <inheritdoc/>
public sealed class JetStreamBootstrapper(INatsJSContext js, IConfiguration configuration) : IHostedService
{
    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var config = new StreamConfig(
            name: configuration.GetSection("Nats")["StreamName"] ?? throw new KeyNotFoundException("StreamName section of Nats is missing"),
            subjects: ["car-rental.>"]
        );
        await js.CreateStreamAsync(config, cancellationToken);
    }
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
