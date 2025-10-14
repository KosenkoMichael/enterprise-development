using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using MongoDB.Driver;

namespace CarRentalService.Infrastructure.Repositories;
/// <summary>
/// Repository implementation for managing <see cref="VehicleModel"/> entities using MongoDB.
/// </summary>
public class VehicleModelRepository: IRepository<VehicleModel>
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<VehicleModel> _collection;

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleModelRepository"/> class.
    /// </summary>
    /// <param name="client">The MongoDB client used to access the database.</param>
    public VehicleModelRepository(IMongoClient client)
    {
        _database = client.GetDatabase("car-rental");
        _collection = _database.GetCollection<VehicleModel>("vehicle-models");
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(VehicleModel entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _collection.DeleteOneAsync(vm => vm.Id == id);
        return result.DeletedCount > 0;
    }

    /// <inheritdoc/>
    public async Task<List<VehicleModel>> ReadAllAsync() =>
        await (await _collection.FindAsync(Builders<VehicleModel>.Filter.Empty)).ToListAsync();
    
    /// <inheritdoc/>
    public async Task<VehicleModel?> ReadAsync(Guid id) =>
        await _collection.Find(vm => vm.Id == id).FirstOrDefaultAsync();
    
    /// <inheritdoc/>
    public async Task<VehicleModel?> UpdateAsync(Guid id, VehicleModel entity)
    {
        return await _collection.FindOneAndReplaceAsync(x => x.Id == id, entity);
    }
}
