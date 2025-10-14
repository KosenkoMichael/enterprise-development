using CarRentalService.WebApplication.Dto;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;

namespace CarRentalService.WebApplication.Services;

/// <summary>
/// Provides operations for managing vehicle models in the car rental system.
/// </summary>
/// <param name="repository">Repository for accessing vehicle model data.</param>
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
    /// <param name="entity">The DTO containing vehicle model information.</param>
    /// <returns>The unique identifier of the newly created vehicle model.</returns>
    public async Task<Guid> CreateVehicleModel(VehicleModelDto entity) =>
        await repository.CreateAsync(MapDto(entity));

    /// <summary>
    /// Retrieves all vehicle models from the system.
    /// </summary>
    /// <returns>A list of all vehicle models.</returns>
    public async Task<List<VehicleModel>> GetVehicleModels() =>
        await repository.ReadAllAsync();

    /// <summary>
    /// Retrieves a specific vehicle model by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model.</param>
    /// <returns>The vehicle model if found; otherwise, null.</returns>
    public async Task<VehicleModel?> GetVehicleModel(Guid id) =>
        await repository.ReadAsync(id);

    /// <summary>
    /// Updates an existing vehicle model.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to update.</param>
    /// <param name="entity">The DTO containing updated vehicle model information.</param>
    /// <returns>The updated vehicle model if successful; otherwise, null.</returns>
    public async Task<VehicleModel?> UpdateVehicleModel(Guid id, VehicleModelDto entity) =>
        await repository.UpdateAsync(id, MapDto(entity));

    /// <summary>
    /// Deletes a vehicle model from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public async Task<bool> DeleteVehicleModel(Guid id) =>
        await repository.DeleteAsync(id);
}
