using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Core.Domain.Service;
/// <summary>
/// Interface for model generation
/// </summary>
public interface IModelGenerationService
{
    /// <summary>
    /// Creates a new model generation for a vehicle model.
    /// </summary>
    /// <param name="entity">The the model generation data.</param>
    /// <returns>The unique identifier of the newly created model generation.</returns>
    public Task<Guid> CreateModelGenerationAsync(ModelGeneration entity);
    /// <summary>
    /// Deletes a model generation from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public Task<bool> DeleteModelGenerationAsync(Guid id);
    /// <summary>
    /// Retrieves a specific model generation by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation.</param>
    /// <returns>The model generation if found; otherwise, null.</returns>
    public Task<ModelGeneration?> GetModelGenerationAsync(Guid id);
    /// <summary>
    /// Retrieves all model generations from the system.
    /// </summary>
    /// <returns>A list of all model generations.</returns>
    public Task<List<ModelGeneration>> GetModelGenerationsAsync();
    /// <summary>
    /// Updates an existing model generation.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to update.</param>
    /// <param name="entity">The updated model generation data.</param>
    /// <returns>The updated model generation if successful; otherwise, null.</returns>
    public Task<ModelGeneration?> UpdateModelGenerationAsync(Guid id, ModelGeneration entity);
}