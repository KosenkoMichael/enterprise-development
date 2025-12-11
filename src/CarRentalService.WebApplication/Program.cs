using CarRentalService.Core.Contracts.Dto;
using CarRentalService.Core.Contracts.Mappers;
using CarRentalService.Core.Domain.DataSeed;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.Core.Domain.Service;
using CarRentalService.Infrastructure;
using CarRentalService.Infrastructure.Nats.Serializing;
using CarRentalService.Infrastructure.Repositories;
using CarRentalService.WebApplication;
using CarRentalService.WebApplication.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using NATS.Client.Core;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

builder.AddMongoDBClient("car-rental");

builder.Services.AddDbContext<CarRentalDbContext>((services, o) =>
{
    var db = services.GetRequiredService<IMongoDatabase>();
    o.UseMongoDB(db.Client, db.DatabaseNamespace.DatabaseName);
});

builder.Services.AddSingleton<DbSeeder>();
builder.Services.AddHostedService<DbSeedService>();

builder.AddNatsClient("car-rental-nats", (sp, opts) =>
{
    opts = opts with
    {
        SerializerRegistry = new CarRentalSerializerRegistry()
    };
    return opts;
});
builder.Services.AddHostedService(provider =>
{
    var connection = provider.GetRequiredService<INatsConnection>();
    var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    var logger = provider.GetRequiredService<ILogger<NatsGenericConsumer<CustomerRequest, ICustomerService>>>();
    var streamName = builder.Configuration.GetSection("Nats")["StreamName"]!;
    return new NatsGenericConsumer<CustomerRequest, ICustomerService>(
        connection,
        scopeFactory,
        "car-rental.customers.*",
        async (service, customer) => await service.CreateCustomerAsync(customer.ToDomain()),
        logger,
        streamName
    );
});

builder.Services.AddHostedService(provider =>
{
    var connection = provider.GetRequiredService<INatsConnection>();
    var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    var logger = provider.GetRequiredService<ILogger<NatsGenericConsumer<VehicleModelRequest, IVehicleModelService>>>();
    var streamName = builder.Configuration.GetSection("Nats")["StreamName"]!;
    return new NatsGenericConsumer<VehicleModelRequest, IVehicleModelService>(
        connection,
        scopeFactory,
        "car-rental.vehiclemodels.*",
        async (service, model) => await service.CreateVehicleModel(model.ToDomain()),
        logger,
        streamName
    );
});

builder.Services.AddHostedService(provider =>
{
    var connection = provider.GetRequiredService<INatsConnection>();
    var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    var logger = provider.GetRequiredService<ILogger<NatsGenericConsumer<ModelGenerationRequest, IModelGenerationService>>>();
    var streamName = builder.Configuration.GetSection("Nats")["StreamName"]!;
    return new NatsGenericConsumer<ModelGenerationRequest, IModelGenerationService>(
        connection,
        scopeFactory,
        "car-rental.modelgenerations.*",
        async (service, modelGen) => await service.CreateModelGenerationAsync(modelGen.ToDomain()),
        logger,
        streamName
    );
});

builder.Services.AddHostedService(provider =>
{
    var connection = provider.GetRequiredService<INatsConnection>();
    var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    var logger = provider.GetRequiredService<ILogger<NatsGenericConsumer<VehicleRequest, IVehicleService>>>();
    var streamName = builder.Configuration.GetSection("Nats")["StreamName"]!;
    return new NatsGenericConsumer<VehicleRequest, IVehicleService>(
        connection,
        scopeFactory,
        "car-rental.vehicles.*",
        async (service, vehicle) => await service.CreateVehicleAsync(vehicle.ToDomain()),
        logger,
        streamName
    );
});

builder.Services.AddHostedService(provider =>
{
    var connection = provider.GetRequiredService<INatsConnection>();
    var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
    var logger = provider.GetRequiredService<ILogger<NatsGenericConsumer<RentalRequest, IRentalService>>>();
    var streamName = builder.Configuration.GetSection("Nats")["StreamName"]!;
    return new NatsGenericConsumer<RentalRequest, IRentalService>(
        connection,
        scopeFactory,
        "car-rental.rentals.*",
        async (service, rental) => await service.CreateRentalAsync(rental.ToDomain()),
        logger,
        streamName
    );
});

builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IRepository<VehicleModel>, VehicleModelRepository>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();

builder.Services.AddScoped<IRepository<ModelGeneration>, ModelGenerationRepository>();
builder.Services.AddScoped<IModelGenerationService, ModelGenerationService>();

builder.Services.AddScoped<IRepository<Vehicle>, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddScoped<IRepository<Rental>, RentalRepository>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
