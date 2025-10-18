using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Core.Domain.Service;

/// <summary>
/// Interface for vehicle model service
/// </summary>
public interface IVehicleModelService
{

    /// <summary>
    /// Creates a new vehicle model.
    /// </summary>
    /// <param name="entity">The vehicle model information.</param>
    /// <returns>The unique identifier of the newly created vehicle model.</returns>
    public Task<Guid> CreateVehicleModel(VehicleModel entity);

    /// <summary>
    /// Deletes a vehicle model from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public Task<bool> DeleteVehicleModel(Guid id);

    /// <summary>
    /// Retrieves a specific vehicle model by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model.</param>
    /// <returns>The vehicle model if found; otherwise, null.</returns>
    public Task<VehicleModel?> GetVehicleModel(Guid id);

    /// <summary>
    /// Retrieves all vehicle models from the system.
    /// </summary>
    /// <returns>A list of all vehicle models.</returns>
    public Task<List<VehicleModel>> GetVehicleModels();

    /// <summary>
    /// Updates an existing vehicle model.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to update.</param>
    /// <param name="entity">The updated vehicle model information.</param>
    /// <returns>The updated vehicle model if successful; otherwise, null.</returns>
    public Task<VehicleModel?> UpdateVehicleModel(Guid id, VehicleModel entity);
}