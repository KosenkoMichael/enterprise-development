using CarRentalService.Core.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using System.Text.Json;
using Bogus;

var builder = WebApplication.CreateBuilder(args);

builder.AddNatsClient("car-rental-nats");
builder.Services.AddSingleton<INatsJSContext>(sp =>
{
    var conn = sp.GetRequiredService<INatsConnection>();
    return new NatsJSContext(conn);
});
builder.Services.AddHostedService<JetStreamBootstrapper>();

builder.Services.AddAntiforgery();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

MapConsumers(app);

app.Run();

static void MapConsumers(WebApplication app)
{
    var customerFaker = new Faker<CustomerRequest>()
        .CustomInstantiator(f => new CustomerRequest(
            $"{f.Random.Number(1, 99):00}{f.Random.String2(2)}{f.Random.Number(100000, 999999):000000}",
            $"{f.Name.LastName()} {f.Name.FirstName()}",
            f.Date.Between(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-18))
        ));

    app.MapPost("/customers", async ([FromForm] int count, INatsJSContext context) =>
    {
        try
        {
            var testCustomers = customerFaker.Generate(count);
            await context.PublishAsync("car-rental.customers.create", JsonSerializer.SerializeToUtf8Bytes(testCustomers));
            return Results.Accepted();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error sending message: {ex.Message}");
        }
    }).DisableAntiforgery();
}

public sealed class JetStreamBootstrapper : IHostedService
{
    private readonly INatsJSContext _js;
    private readonly IConfiguration _configuration;

    public JetStreamBootstrapper(INatsJSContext js, IConfiguration configuration)
    {
        _js = js;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var config = new StreamConfig(
            name: _configuration.GetSection("Nats")["StreamName"] ?? throw new KeyNotFoundException("StreamName section of Nats is missing"),
            //subjects: [_configuration.GetSection("Nats")["SubjectName"] ?? throw new KeyNotFoundException("SubjectName section of Nats is missing")]
            subjects: ["car-rental.>"]
        );
        await _js.CreateStreamAsync(config, cancellationToken);
    }
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
