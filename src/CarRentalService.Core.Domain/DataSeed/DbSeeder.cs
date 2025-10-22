using CarRentalService.Core.Domain.Models;
using MongoDB.Driver;

namespace CarRentalService.Core.Domain.DataSeed;

/// <summary>
/// Database seeder
/// </summary>
/// <param name="dbClient">MongoDb client</param>
public class DbSeeder(IMongoClient dbClient)
{
    private readonly IMongoDatabase _database = dbClient.GetDatabase("car-rental");

    /// <summary>
    /// Seed database with test data
    /// </summary>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>Task result</returns>
    public async Task Seed(CancellationToken cancellationToken)
    {
        var collections = (await (await _database.ListCollectionsAsync(cancellationToken: cancellationToken)).ToListAsync(cancellationToken: cancellationToken)).Select(x => x["name"].AsString)
                                                                                                                                                               .ToHashSet();

        var generator = new DataSeeder();
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
}
