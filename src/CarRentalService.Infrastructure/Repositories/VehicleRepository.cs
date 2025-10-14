using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using MongoDB.Driver;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing <see cref="Vehicle"/> entities using MongoDB.
/// </summary>
public class VehicleRepository : IRepository<Vehicle>
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Vehicle> _collection;

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
    /// </summary>
    /// <param name="client">The MongoDB client used to access the database.</param>
    public VehicleRepository(IMongoClient client)
    {
        _database = client.GetDatabase("car-rental");
        _collection = _database.GetCollection<Vehicle>("vehicles");
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(Vehicle entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _collection.DeleteOneAsync(c => c.Id == id);
        return result.DeletedCount > 0;
    }

    /// <inheritdoc/>
    public async Task<List<Vehicle>> ReadAllAsync() =>
         await (await _collection.FindAsync(Builders<Vehicle>.Filter.Empty)).ToListAsync();

    /// <inheritdoc/>
    public async Task<Vehicle?> ReadAsync(Guid id) =>
        await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<Vehicle?> UpdateAsync(Guid id, Vehicle entity)
    {
        return await _collection.FindOneAndReplaceAsync(x => x.Id == id, entity);
    }
}
