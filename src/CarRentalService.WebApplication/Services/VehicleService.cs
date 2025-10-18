using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Mappers;

namespace CarRentalService.WebApplication.Services;

/// <summary>
/// Provides operations for managing vehicles in the car rental system.
/// </summary>
/// <param name="vehicleRepository">Repository for accessing vehicle data.</param>
/// <param name="modelGenerationRepository">Repository for validating and accessing model generation data.</param>
public class VehicleService(
    IRepository<Vehicle> vehicleRepository,
    IRepository<ModelGeneration> modelGenerationRepository
    )
{
    /// <summary>
    /// Creates a new vehicle.
    /// </summary>
    /// <param name="entity">The DTO containing vehicle information.</param>
    /// <returns>The unique identifier of the newly created vehicle.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the specified model generation does not exist.</exception>
    public async Task<Guid> CreateVehicleAsync(VehicleRequest entity)
    {
        if (await modelGenerationRepository.ReadAsync(entity.ModelGenerationId) is null)
        {
            throw new KeyNotFoundException($"ModelGeneration with Id = {entity.ModelGenerationId} does not found");
        }

        return await vehicleRepository.CreateAsync(entity.ToDomain());
    }

    /// <summary>
    /// Retrieves all vehicles from the system.
    /// </summary>
    /// <returns>A list of all vehicles.</returns>
    public async Task<List<Vehicle>> GetVehiclesAsync() =>
        await vehicleRepository.ReadAllAsync();

    /// <summary>
    /// Retrieves a specific vehicle by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle.</param>
    /// <returns>The vehicle if found; otherwise, null.</returns>
    public async Task<Vehicle?> GetVehicleAsync(Guid id) =>
        await vehicleRepository.ReadAsync(id);

    /// <summary>
    /// Updates an existing vehicle.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to update.</param>
    /// <param name="entity">The DTO containing updated vehicle information.</param>
    /// <returns>The updated vehicle if successful; otherwise, null.</returns>
    public async Task<Vehicle?> UpdateVehicleAsync(Guid id, VehicleRequest entity) =>
        await vehicleRepository.UpdateAsync(id, entity.ToDomain());

    /// <summary>
    /// Deletes a vehicle from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public async Task<bool> DeleteVehicleAsync(Guid id) =>
        await vehicleRepository.DeleteAsync(id);
}
