using CarRentalService.WebApplication.Services;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.Infrastructure.Repositories;
using CarRentalService.WebApplication;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddHostedService<DatabaseSeeder>();

builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<CustomerService>();

builder.Services.AddScoped<IRepository<VehicleModel>, VehicleModelRepository>();
builder.Services.AddScoped<VehicleModelService>();

builder.Services.AddScoped<IRepository<ModelGeneration>, ModelGenerationRepository>();
builder.Services.AddScoped<ModelGenerationService>();

builder.Services.AddScoped<IRepository<Vehicle>, VehicleRepository>();
builder.Services.AddScoped<VehicleService>();

builder.Services.AddScoped<IRepository<Rental>, RentalRepository>();
builder.Services.AddScoped<RentalService>();

builder.Services.AddScoped<AnalyticsService>();

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
