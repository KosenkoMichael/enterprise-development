using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using MongoDB.Driver;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// In-memory implementation of the model generation repository.
/// </summary>
public class ModelGenerationRepository : IRepository<ModelGeneration>
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<ModelGeneration> _collection;

    /// <summary>
    /// Initializes a new instance of the model generation repository.
    /// </summary>
    /// <param name="seeder">Seeder for initial data</param>
    public ModelGenerationRepository(IMongoClient client)
    {
        _database = client.GetDatabase("car-rental");
        _collection = _database.GetCollection<ModelGeneration>("model-generations");
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(ModelGeneration entity)
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
    public async Task<List<ModelGeneration>> ReadAllAsync() =>
         await (await _collection.FindAsync(Builders<ModelGeneration>.Filter.Empty)).ToListAsync();

    /// <inheritdoc/>
    public async Task<ModelGeneration?> ReadAsync(Guid id) =>
        await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<ModelGeneration?> UpdateAsync(Guid id, ModelGeneration entity)
    {
        return await _collection.FindOneAndReplaceAsync(x => x.Id == id, entity);
    }
}