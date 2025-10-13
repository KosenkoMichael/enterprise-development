using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.Infrastructure.InMemory.Seeders;

namespace CarRentalService.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory implementation of the model generation repository.
/// </summary>
public class InMemoryModelGenerationRepository : IRepository<ModelGeneration>
{
    private readonly List<ModelGeneration> _modelGenerations = new();

    /// <summary>
    /// Initializes a new instance of the model generation repository.
    /// </summary>
    /// <param name="seeder">Seeder for initial data</param>
    public InMemoryModelGenerationRepository(ModelGenerationSeeder? seeder)
    {
        if (seeder == null) return;
        _modelGenerations = seeder.GetItems();
    }

    /// <inheritdoc/>
    public Guid Create(ModelGeneration entity)
    {
        _modelGenerations.Add(entity);
        return entity.Id;
    }

    /// <inheritdoc/>
    public bool Delete(Guid id)
    {
        var modelGeneration = _modelGenerations.FirstOrDefault(mg => mg.Id == id);
        if (modelGeneration == null)
        {
            return false;
        }
        _modelGenerations.Remove(modelGeneration);
        return true;
    }

    /// <inheritdoc/>
    public List<ModelGeneration> Read() =>
        [.. _modelGenerations];

    /// <inheritdoc/>
    public ModelGeneration? Read(Guid id) =>
        _modelGenerations.FirstOrDefault(mg => mg.Id == id);

    /// <inheritdoc/>
    public ModelGeneration? Update(Guid id, ModelGeneration entity)
    {
        var index = _modelGenerations.FindIndex(mg => mg.Id == id);
        if (index == -1)
        {
            return null;
        }
        entity.Id = id;
        _modelGenerations[index] = entity;
        return entity;
    }
}