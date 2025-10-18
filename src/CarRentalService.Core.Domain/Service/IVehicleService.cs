using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Core.Domain.Service;

/// <summary>
/// Interface for vehicle service
/// </summary>
public interface IVehicleService
{
    /// <summary>
    /// Creates a new vehicle
    /// </summary>
    /// <param name="entity">The vehicle information.</param>
    /// <returns></returns>
    public Task<Guid> CreateVehicleAsync(Vehicle entity);

    /// <summary>
    /// Deletes a vehicle from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public Task<bool> DeleteVehicleAsync(Guid id);
    /// <summary>
    /// Retrieves a specific vehicle by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle.</param>
    /// <returns>The vehicle if found; otherwise, null.</returns>
    public Task<Vehicle?> GetVehicleAsync(Guid id);
    /// <summary>
    /// Retrieves all vehicles from the system.
    /// </summary>
    /// <returns>A list of all vehicles.</returns>
    public Task<List<Vehicle>> GetVehiclesAsync();
    /// <summary>
    /// Updates an existing vehicle.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to update.</param>
    /// <param name="entity">The updated vehicle information.</param>
    /// <returns>The updated vehicle if successful; otherwise, null.</returns>
    public Task<Vehicle?> UpdateVehicleAsync(Guid id, Vehicle entity);
}