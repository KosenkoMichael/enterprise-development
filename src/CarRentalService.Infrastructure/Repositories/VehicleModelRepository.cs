using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CarRentalService.Infrastructure.Repositories;
/// <summary>
/// Repository implementation for managing <see cref="VehicleModel"/> entities using MongoDB.
/// </summary>
public class VehicleModelRepository(CarRentalDbContext dbContext) : IRepository<VehicleModel>
{
    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(VehicleModel entity)
    {
        dbContext.VehicleModels.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await ReadAsync(id);
        if (entity == null) return false;
        var result = dbContext.VehicleModels.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }
    /// <inheritdoc/>
    public async Task<List<VehicleModel>> ReadAllAsync() =>
        await dbContext.VehicleModels.AsNoTracking().ToListAsync();
    /// <inheritdoc/>
    public async Task<VehicleModel?> ReadAsync(Guid id) =>
        await dbContext.VehicleModels.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    /// <inheritdoc/>
    public async Task<VehicleModel?> UpdateAsync(Guid id, VehicleModel entity)
    {
        var vehicleModel = await dbContext.VehicleModels.FirstOrDefaultAsync(x => x.Id == id);

        if (vehicleModel == null) return null;

        entity.Id = vehicleModel.Id;

        dbContext.VehicleModels.Update(entity);

        await dbContext.SaveChangesAsync();
        return vehicleModel;
    }
}
