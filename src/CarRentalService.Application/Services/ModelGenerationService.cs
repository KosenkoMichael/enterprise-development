using CarRentalService.Application.Dto;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing model generation operations.
/// </summary>
/// <param name="modelGenerationRepository">Model generation repository instance</param>
/// <param name="vehicleModelRepository">Vehicle model repository instance</param>
public class ModelGenerationService(
    IRepository<ModelGeneration> modelGenerationRepository,
    IRepository<VehicleModel> vehicleModelRepository
    )
{
    private ModelGeneration MapDto(ModelGenerationDto entity)
    {
        return new ModelGeneration
        {
            Year = entity.Year,
            EngineVolume = entity.EngineVolume,
            TransmissionType = entity.TransmissionType,
            VehicleModelId = entity.VehicleModelId,
            RentalPricePerHour = entity.RentalPricePerHour
        };
    }

    /// <summary>
    /// Creates a new model generation.
    /// </summary>
    /// <param name="entity">Model generation data</param>
    /// <returns>Unique identifier of created model generation</returns>
    public async Task<Guid> CreateModelGenerationAsync(ModelGenerationDto entity)
    {
        if (await vehicleModelRepository.ReadAsync(entity.VehicleModelId) is null)
        {
            throw new Exception($"VehicleModel with Id = {entity.VehicleModelId} does not exist");
        }

        return await modelGenerationRepository.CreateAsync(MapDto(entity));
    }

    /// <summary>
    /// Retrieves all model generations.
    /// </summary>
    /// <returns>List of all model generations</returns>
    public async Task<List<ModelGeneration>> GetModelGenerationsAsync() =>
        await modelGenerationRepository.ReadAllAsync();

    /// <summary>
    /// Retrieves a specific model generation by ID.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    /// <returns>Model generation if found; otherwise null</returns>
    public async Task<ModelGeneration?> GetModelGenerationAsync(Guid id) =>
        await modelGenerationRepository.ReadAsync(id);

    /// <summary>
    /// Updates an existing model generation.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    /// <param name="entity">Updated model generation data</param>
    /// <returns>Updated model generation if found; otherwise null</returns>
    public async Task<ModelGeneration?> UpdateModelGenerationAsync(Guid id, ModelGenerationDto entity) =>
        await modelGenerationRepository.UpdateAsync(id, MapDto(entity));

    /// <summary>
    /// Deletes a model generation by ID.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    /// <returns>True if deleted successfully; otherwise false</returns>
    public async Task<bool> DeleteModelGenerationAsync(Guid id) =>
        await modelGenerationRepository.DeleteAsync(id);
}