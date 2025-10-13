using CarRentalService.Application.Dto;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing vehicle model operations.
/// </summary>
/// <param name="repository">Vehicle model repository instance</param>
public class VehicleModelService(IRepository<VehicleModel> repository)
{
    private static VehicleModel MapDto(VehicleModelDto entity)
    {
        return new VehicleModel
        {
            Name = entity.Name,
            DriveType = entity.DriveType,
            SeatCount = entity.SeatCount,
            BodyType = entity.BodyType,
            Class = entity.Class
        };
    }

    /// <summary>
    /// Creates a new vehicle model.
    /// </summary>
    /// <param name="entity">Vehicle model data to create</param>
    /// <returns>Unique identifier of created vehicle model</returns>
    public async Task<Guid> CreateVehicleModel(VehicleModelDto entity) =>
        await repository.CreateAsync(MapDto(entity));

    /// <summary>
    /// Retrieves all vehicle models.
    /// </summary>
    /// <returns>List of all vehicle models</returns>
    public async Task<List<VehicleModel>> GetVehicleModels() =>
        await repository.ReadAllAsync();

    /// <summary>
    /// Retrieves a specific vehicle model by ID.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    /// <returns>Vehicle model if found; otherwise null</returns>
    public async Task<VehicleModel?> GetVehicleModel(Guid id) =>
        await repository.ReadAsync(id);

    /// <summary>
    /// Updates an existing vehicle model.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    /// <param name="entity">Updated vehicle model data</param>
    /// <returns>Updated vehicle model if found; otherwise null</returns>
    public async Task<VehicleModel?> UpdateVehicleModel(Guid id, VehicleModelDto entity) =>
        await repository.UpdateAsync(id, MapDto(entity));

    /// <summary>
    /// Deletes a vehicle model by ID.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    /// <returns>True if deleted successfully; otherwise false</returns>
    public async Task<bool> DeleteVehicleModel(Guid id) =>
        await repository.DeleteAsync(id);
}