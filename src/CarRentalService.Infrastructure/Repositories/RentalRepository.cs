using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing <see cref="Rental"/> entities using MongoDB.
/// Calculates the total cost of rentals based on the vehicle's model generation rental price.
/// </summary>
public class RentalRepository(CarRentalDbContext dbContext) : IRepository<Rental>
{
    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(Rental entity)
    {
        dbContext.Rentals.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await dbContext.Rentals.Where(x => x.Id == id).ExecuteDeleteAsync();
        return result > 0;
    }

    /// <inheritdoc/>
    public async Task<List<Rental>> ReadAllAsync()
    {
        var rentals = await dbContext.Rentals.ToListAsync();
        foreach (var rental in rentals)
        {
            var vehicle = await dbContext.Vehicles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == rental.VehicleId);
            if (vehicle is null) continue;
            var modelGeneration = await dbContext.ModelGenerations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == vehicle.ModelGenerationId);
            if (modelGeneration is null) continue;
            rental.TotalCost = (decimal)rental.RentalDurationHours * modelGeneration.RentalPricePerHour;
        }

        return rentals;
    }
    /// <inheritdoc/>
    public async Task<Rental?> ReadAsync(Guid id)
    {
        var rental = await dbContext.Rentals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (rental is null) return null;
        var vehicle = await dbContext.Vehicles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == rental.VehicleId);
        if (vehicle is null) return null;
        var modelGeneration = await dbContext.ModelGenerations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == vehicle.ModelGenerationId);
        if (modelGeneration is null) return null;
        rental.TotalCost = (decimal)rental.RentalDurationHours * modelGeneration.RentalPricePerHour;

        return rental;
    }

    /// <inheritdoc/>
    public async Task<Rental?> UpdateAsync(Guid id, Rental entity)
    {
        var rental = await dbContext.Rentals.FirstOrDefaultAsync(x => x.Id == id);

        if (rental == null) return null;

        entity.Id = rental.Id;

        dbContext.Rentals.Update(entity);

        await dbContext.SaveChangesAsync();
        return rental;
    }
}
