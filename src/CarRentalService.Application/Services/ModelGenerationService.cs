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
        var vehicleModel = vehicleModelRepository.Read(entity.VehicleModelId);

        if (vehicleModel == null)
        {
            throw new InvalidOperationException($"VehicleModel с Id {entity.VehicleModelId} does not found");
        }
        return new ModelGeneration
        {
            Year = entity.Year,
            EngineVolume = entity.EngineVolume,
            TransmissionType = entity.TransmissionType,
            Model = vehicleModel,
            RentalPricePerHour = entity.RentalPricePerHour
        };
    }

    /// <summary>
    /// Creates a new model generation.
    /// </summary>
    /// <param name="entity">Model generation data</param>
    /// <returns>Unique identifier of created model generation</returns>
    public Guid CreateModelGeneration(ModelGenerationDto entity) =>
        modelGenerationRepository.Create(MapDto(entity));

    /// <summary>
    /// Retrieves all model generations.
    /// </summary>
    /// <returns>List of all model generations</returns>
    public List<ModelGeneration> GetModelGenerations() =>
        modelGenerationRepository.Read();

    /// <summary>
    /// Retrieves a specific model generation by ID.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    /// <returns>Model generation if found; otherwise null</returns>
    public ModelGeneration? GetModelGeneration(Guid id) =>
        modelGenerationRepository.Read(id);

    /// <summary>
    /// Updates an existing model generation.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    /// <param name="entity">Updated model generation data</param>
    /// <returns>Updated model generation if found; otherwise null</returns>
    public ModelGeneration? UpdateModelGeneration(Guid id, ModelGenerationDto entity) =>
        modelGenerationRepository.Update(id, MapDto(entity));

    /// <summary>
    /// Deletes a model generation by ID.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    /// <returns>True if deleted successfully; otherwise false</returns>
    public bool DeleteModelGeneration(Guid id) =>
        modelGenerationRepository.Delete(id);
}