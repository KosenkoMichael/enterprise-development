using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using MongoDB.Driver;

namespace CarRentalService.Infrastructure.Repositories;
/// <summary>
/// Implementation of the rental repository
/// </summary>
public class RentalRepository : IRepository<Rental>
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Rental> _rentalColection;

    /// <summary>
    /// Initializes a new instance of the rental repository.
    /// </summary>
    /// <param name="client">MongoDB client</param>
    public RentalRepository(IMongoClient client)
    {
        _database = client.GetDatabase("car-rental");
        _rentalColection = _database.GetCollection<Rental>("rentals");
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(Rental entity)
    {
        await _rentalColection.InsertOneAsync(entity);
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _rentalColection.DeleteOneAsync(c => c.Id == id);
        return result.DeletedCount > 0;
    }

    /// <inheritdoc/>
    public async Task<List<Rental>> ReadAllAsync() =>
         await (await _rentalColection.FindAsync(Builders<Rental>.Filter.Empty)).ToListAsync();

    /// <inheritdoc/>
    public async Task<Rental?> ReadAsync(Guid id) =>
        await _rentalColection.Find(c => c.Id == id).FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<Rental?> UpdateAsync(Guid id, Rental entity)
    {
        return await _rentalColection.FindOneAndReplaceAsync(x => x.Id == id, entity);
    }
}
