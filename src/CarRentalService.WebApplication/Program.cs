using CarRentalService.Application.Services;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.Infrastructure.InMemory.Repositories;
using CarRentalService.Infrastructure.InMemory.Seeders;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddSingleton<CustomerSeeder>();
builder.Services.AddSingleton<IRepository<Customer>, InMemoryCustomerRepository>();
builder.Services.AddTransient<CustomerService>();

builder.Services.AddSingleton<VehicleModelSeeder>();
builder.Services.AddSingleton<IRepository<VehicleModel>, InMemoryVehicleModelRepository>();
builder.Services.AddTransient<VehicleModelService>();

builder.Services.AddSingleton<ModelGenerationSeeder>();
builder.Services.AddSingleton<IRepository<ModelGeneration>, InMemoryModelGenerationRepository>();
builder.Services.AddTransient<ModelGenerationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
