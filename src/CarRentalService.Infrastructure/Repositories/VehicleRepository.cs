using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing <see cref="Vehicle"/> entities using MongoDB.
/// </summary>
public class VehicleRepository(CarRentalDbContext dbContext) : IRepository<Vehicle>
{
    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(Vehicle entity)
    {
        dbContext.Vehicles.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await ReadAsync(id);
        if (entity == null) return false;
        var result = dbContext.Vehicles.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }
    /// <inheritdoc/>
    public async Task<List<Vehicle>> ReadAllAsync() =>
        await dbContext.Vehicles.AsNoTracking().ToListAsync();
    /// <inheritdoc/>
    public async Task<Vehicle?> ReadAsync(Guid id) =>
        await dbContext.Vehicles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    /// <inheritdoc/>
    public async Task<Vehicle?> UpdateAsync(Guid id, Vehicle entity)
    {
        var vehicle = await dbContext.Vehicles.FirstOrDefaultAsync(x => x.Id == id);

        if (vehicle == null) return null;

        entity.Id = vehicle.Id;

        dbContext.Vehicles.Update(entity);

        await dbContext.SaveChangesAsync();
        return vehicle;
    }
}
