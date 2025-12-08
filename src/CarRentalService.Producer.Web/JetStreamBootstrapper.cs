using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;

/// <summary>
/// JetStream bootstrapper for NATS
/// </summary>
public sealed class JetStreamBootstrapper : IHostedService
{
    private readonly INatsJSContext _js;
    private readonly IConfiguration _configuration;

    /// <inheritdoc/>
    public JetStreamBootstrapper(INatsJSContext js, IConfiguration configuration)
    {
        _js = js;
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var config = new StreamConfig(
            name: _configuration.GetSection("Nats")["StreamName"] ?? throw new KeyNotFoundException("StreamName section of Nats is missing"),
            subjects: ["car-rental.>"]
        );
        await _js.CreateStreamAsync(config, cancellationToken);
    }
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
