using NATS.Client.Core;
using NATS.Client.JetStream;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddLogging();

builder.AddNatsClient("car-rental-nats");

builder.Services.AddSingleton<INatsJSContext>(sp =>
{
    var conn = sp.GetRequiredService<INatsConnection>();
    return new NatsJSContext(conn);
});

builder.Services.AddHttpClient("CarRentalApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7010");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNameCaseInsensitive = true;
    options.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddHostedService<JetStreamBootstrapper>();

builder.Services.AddControllers();

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

app.MapControllers();

app.Run();