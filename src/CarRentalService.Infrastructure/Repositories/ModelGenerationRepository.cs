using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing <see cref="ModelGeneration"/> entities using MongoDB.
/// </summary>
public class ModelGenerationRepository(CarRentalDbContext dbContext) : IRepository<ModelGeneration>
{
    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(ModelGeneration entity)
    {
        dbContext.ModelGenerations.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await dbContext.ModelGenerations.Where(x => x.Id == id).ExecuteDeleteAsync();
        return result > 0;
    }
    /// <inheritdoc/>
    public async Task<List<ModelGeneration>> ReadAllAsync() =>
        await dbContext.ModelGenerations.ToListAsync();
    /// <inheritdoc/>
    public async Task<ModelGeneration?> ReadAsync(Guid id) =>
        await dbContext.ModelGenerations.FirstOrDefaultAsync(x => x.Id == id);
    /// <inheritdoc/>
    public async Task<ModelGeneration?> UpdateAsync(Guid id, ModelGeneration entity)
    {
        var modelGeneration = await dbContext.ModelGenerations.FirstOrDefaultAsync(x => x.Id == id);

        if (modelGeneration == null) return null;

        entity.Id = modelGeneration.Id;

        dbContext.ModelGenerations.Update(entity);

        await dbContext.SaveChangesAsync();
        return modelGeneration;
    }
}