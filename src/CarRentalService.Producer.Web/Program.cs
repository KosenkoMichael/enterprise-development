using Bogus;
using CarRentalService.Core.Contracts.Dto;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNameCaseInsensitive = true;
    options.Converters.Add(new JsonStringEnumConverter());
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

MapCustomersEndpoint(app);
MapModelGenerationsEndpoint(app);
MapRentalEndpoint(app);
MapVehicleEndpoint(app);
MapVehicleModelEndpoint(app);

app.Run();

static void MapCustomersEndpoint(WebApplication app)
{
    var customerFaker = new Faker<CustomerRequest>()
        .CustomInstantiator(f => new CustomerRequest(
            DriverLicenseNumber: $"{f.Random.Number(1, 99):00}{f.Random.String2(2)}{f.Random.Number(100000, 999999):000000}",
            FullName: $"{f.Name.LastName()} {f.Name.FirstName()}",
            DateOfBirth: f.Date.Between(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-18))
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

static void MapModelGenerationsEndpoint(WebApplication app)
{
    app.MapPost("/modelgenerations", async ([FromForm] int count, INatsJSContext context, IHttpClientFactory httpClientFactory, IOptions<JsonSerializerOptions> jsonOptions) =>
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("CarRentalApi");
            var response = await httpClient.GetFromJsonAsync<VehicleModelCollectionResponse>(
                "/api/vehiclemodels",
                jsonOptions.Value
            );
            if (response == null)
                return Results.Problem(
                    detail: "Vehicle model list is empty",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            var existingVehicleModelIds = response.VehicleModels
                .Select(vm => vm.Id)
                .ToList();
            var modelGenerationFaker = new Faker<ModelGenerationRequest>()
            .CustomInstantiator(f => new ModelGenerationRequest(
                Year: f.Random.Int(2000, 2025),
                EngineVolume: Math.Round(f.Random.Double(1.0, 5.0), 1),
                TransmissionType: f.PickRandom<TransmissionType>(),
                VehicleModelId: f.PickRandom(existingVehicleModelIds),
                RentalPricePerHour: f.Random.Decimal(10, 500)
            ));

            var testModelGenerations = modelGenerationFaker.Generate(count);
            await context.PublishAsync("car-rental.modelgenerations.create", JsonSerializer.SerializeToUtf8Bytes(testModelGenerations));
            return Results.Accepted();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error sending message: {ex.Message}");
        }
    }).DisableAntiforgery();
}

static void MapRentalEndpoint(WebApplication app)
{
    app.MapPost("/rentals", async ([FromForm] int count, INatsJSContext context, IHttpClientFactory httpClientFactory, IOptions<JsonSerializerOptions> jsonOptions) =>
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("CarRentalApi");
            var customerResponse = await httpClient.GetFromJsonAsync<CustomerCollectionResponse>(
                "/api/customers",
                jsonOptions.Value
            );
            var vehicleResponse = await httpClient.GetFromJsonAsync<VehicleCollectionResponse>(
                "/api/vehicles",
                jsonOptions.Value
            );
            if (customerResponse == null)
                return Results.Problem(
                    detail: "Customer list is empty",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            if (vehicleResponse == null)
                return Results.Problem(
                    detail: "Vehicle list is empty",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            var existingVehicleIds = vehicleResponse.Vehicles
                .Select(vm => vm.Id)
                .ToList();
            var existingCustomerIds = customerResponse.Customers
                .Select(vm => vm.Id)
                .ToList();
            var rentalFaker = new Faker<RentalRequest>()
            .CustomInstantiator(f => new RentalRequest(
                VehicleId: f.PickRandom(existingVehicleIds),
                CustomerId: f.PickRandom(existingCustomerIds),
                RentStartTime: f.Date.Past(60),
                RentalDurationHours: Math.Round(f.Random.Double(0.5, 12.0) * 2) / 2
            ));

            var testRental = rentalFaker.Generate(count);
            await context.PublishAsync("car-rental.rentals.create", JsonSerializer.SerializeToUtf8Bytes(testRental));
            return Results.Accepted();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error sending message: {ex.Message}");
        }
    }).DisableAntiforgery();
}

static void MapVehicleEndpoint(WebApplication app)
{
    app.MapPost("/vehicles", async ([FromForm] int count, INatsJSContext context, IHttpClientFactory httpClientFactory, IOptions<JsonSerializerOptions> jsonOptions) =>
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient("CarRentalApi");
            var response = await httpClient.GetFromJsonAsync<ModelGenerationCollectionResponse>(
                "/api/modelgenerations",
                jsonOptions.Value
            );
            if (response == null)
                return Results.Problem(
                    detail: "Modelgeneration list is empty",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            var existingModelGenerationIds = response.ModelGenerations
                .Select(vm => vm.Id)
                .ToList();
            var goodColors = Enum.GetValues<KnownColor>()
                .Where(kc =>
                    !Color.FromKnownColor(kc).IsSystemColor &&
                    !Color.FromKnownColor(kc).Name.StartsWith("ff"))
                .ToList();
            var vehicleFaker = new Faker<VehicleRequest>()
            .CustomInstantiator(f => new VehicleRequest(
                ModelGenerationId: f.PickRandom(existingModelGenerationIds),
                LicensePlate: $"{f.Random.String2(1)}{f.Random.Number(000, 999):000}{f.Random.String2(2)}",
                Color: Color.FromKnownColor(f.PickRandom(goodColors)).Name
            ));

            var testVehicle = vehicleFaker.Generate(count);
            await context.PublishAsync("car-rental.vehicles.create", JsonSerializer.SerializeToUtf8Bytes(testVehicle));
            return Results.Accepted();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error sending message: {ex.Message}");
        }
    }).DisableAntiforgery();
}

static void MapVehicleModelEndpoint(WebApplication app)
{
    var vehicleModelFaker = new Faker<VehicleModelRequest>()
        .CustomInstantiator(f => new VehicleModelRequest(
            Name: f.Vehicle.Manufacturer(),
            DriveType: f.PickRandom<CarRentalService.Core.Domain.Models.DriveType>(),
            SeatCount: f.Random.Int(1, 4),
            BodyType: f.PickRandom<BodyType>(),
            Class: f.PickRandom<VehicleClass>()
        ));

    app.MapPost("/vehiclemodels", async ([FromForm] int count, INatsJSContext context) =>
    {
        try
        {
            var testVehicleModels = vehicleModelFaker.Generate(count);
            await context.PublishAsync("car-rental.vehiclemodels.create", JsonSerializer.SerializeToUtf8Bytes(testVehicleModels));
            return Results.Accepted();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error sending message: {ex.Message}");
        }
    }).DisableAntiforgery();
}
