using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.TestData;
using MongoDB.Driver;

namespace CarRentalService.WebApplication;
/// <summary>
/// DB seeder
/// </summary>

public class DBSeeder : IHostedService
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;

    /// <summary>
    /// Mongo client
    /// </summary>
    /// <param name="client"></param>
    public DBSeeder(IMongoClient client)
    {
        _client = client;
        _database = _client.GetDatabase("car-rental");
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var collections = (await (await _database.ListCollectionsAsync(cancellationToken: cancellationToken)).ToListAsync()).Select(x => x["name"].AsString).ToHashSet();

        var generator = new TestDataGenerator();
        var (vehicleModels, modelGenerations, vehicles, customers, rentals) = generator.GenerateTestData();

        if (!collections.Contains("customers"))
        {
            var customerCollection = _database.GetCollection<Customer>("customers");
            await customerCollection.InsertManyAsync(customers, cancellationToken: cancellationToken);
        }

        if (!collections.Contains("vehicle-models"))
        {
            var vehicleModelCollection = _database.GetCollection<VehicleModel>("vehicle-models");
            await vehicleModelCollection.InsertManyAsync(vehicleModels, cancellationToken: cancellationToken);
        }

        if (!collections.Contains("model-generations"))
        {
            var modelGenerationCollection = _database.GetCollection<ModelGeneration>("model-generations");
            await modelGenerationCollection.InsertManyAsync(modelGenerations, cancellationToken: cancellationToken);
        }

        if (!collections.Contains("vehicles"))
        {
            var vehicleCollection = _database.GetCollection<Vehicle>("vehicles");
            await vehicleCollection.InsertManyAsync(vehicles, cancellationToken: cancellationToken);
        }

        if (!collections.Contains("rentals"))
        {
            var rentalCollection = _database.GetCollection<Rental>("rentals");
            await rentalCollection.InsertManyAsync(rentals, cancellationToken: cancellationToken);
        }

    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken) =>
        Task.CompletedTask;
}
