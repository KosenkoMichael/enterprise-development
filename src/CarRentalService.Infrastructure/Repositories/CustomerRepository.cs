using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using MongoDB.Driver;

namespace CarRentalService.Infrastructure.Repositories;
/// <summary>
/// Repository implementation for managing <see cref="Customer"/> entities using MongoDB.
/// </summary>
public class CustomerRepository : IRepository<Customer>
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Customer> _collection;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
    /// </summary>
    /// <param name="client">The MongoDB client used to access the database.</param>
    public CustomerRepository(IMongoClient client)
    {
        _database = client.GetDatabase("car-rental");
        _collection = _database.GetCollection<Customer>("customers");
    }
    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(Customer entity)
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
    public async Task<List<Customer>> ReadAllAsync() =>
        await (await _collection.FindAsync(Builders<Customer>.Filter.Empty)).ToListAsync();
    /// <inheritdoc/>
    public async Task<Customer?> ReadAsync(Guid id) =>
        await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
    /// <inheritdoc/>
    public async Task<Customer?> UpdateAsync(Guid id, Customer entity)
    {
        return await _collection.FindOneAndReplaceAsync(x => x.Id == id, entity);
    }
}
