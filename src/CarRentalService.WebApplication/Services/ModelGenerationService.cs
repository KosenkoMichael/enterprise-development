using CarRentalService.WebApplication.Dto;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.Core.Domain.Models;
using CarRentalService.WebApplication.Mappers;
using CarRentalService.Core.Domain.Service;

namespace CarRentalService.WebApplication.Services;

/// <summary>
/// Provides operations for managing vehicle model generations in the car rental system.
/// </summary>
/// <param name="modelGenerationRepository">Repository for accessing model generation data.</param>
/// <param name="vehicleModelRepository">Repository for validating existence of vehicle models.</param>
public class ModelGenerationService(
    IRepository<ModelGeneration> modelGenerationRepository,
    IRepository<VehicleModel> vehicleModelRepository
    ) : IModelGenerationService
{
    /// <summary>
    /// Creates a new model generation for a vehicle model.
    /// </summary>
    /// <param name="entity">The the model generation data.</param>
    /// <returns>The unique identifier of the newly created model generation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the specified vehicle model does not exist.</exception>
    public async Task<Guid> CreateModelGenerationAsync(ModelGeneration entity)
    {
        if (await vehicleModelRepository.ReadAsync(entity.VehicleModelId) is null)
        {
            throw new KeyNotFoundException($"VehicleModel with Id = {entity.VehicleModelId} does not exist");
        }

        return await modelGenerationRepository.CreateAsync(entity);
    }

    /// <summary>
    /// Retrieves all model generations from the system.
    /// </summary>
    /// <returns>A list of all model generations.</returns>
    public async Task<List<ModelGeneration>> GetModelGenerationsAsync() =>
        await modelGenerationRepository.ReadAllAsync();

    /// <summary>
    /// Retrieves a specific model generation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation.</param>
    /// <returns>The model generation if found; otherwise, null.</returns>
    public async Task<ModelGeneration?> GetModelGenerationAsync(Guid id) =>
        await modelGenerationRepository.ReadAsync(id);

    /// <summary>
    /// Updates an existing model generation.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to update.</param>
    /// <param name="entity">The updated model generation data.</param>
    /// <returns>The updated model generation if successful; otherwise, null.</returns>
    public async Task<ModelGeneration?> UpdateModelGenerationAsync(Guid id, ModelGeneration entity) =>
        await modelGenerationRepository.UpdateAsync(id, entity);

    /// <summary>
    /// Deletes a model generation from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public async Task<bool> DeleteModelGenerationAsync(Guid id) =>
        await modelGenerationRepository.DeleteAsync(id);
}
