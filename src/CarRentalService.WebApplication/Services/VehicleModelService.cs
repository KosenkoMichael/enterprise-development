using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Mappers;

namespace CarRentalService.WebApplication.Services;

/// <summary>
/// Provides operations for managing vehicle models in the car rental system.
/// </summary>
/// <param name="repository">Repository for accessing vehicle model data.</param>
public class VehicleModelService(IRepository<VehicleModel> repository)
{
    /// <summary>
    /// Creates a new vehicle model.
    /// </summary>
    /// <param name="entity">The DTO containing vehicle model information.</param>
    /// <returns>The unique identifier of the newly created vehicle model.</returns>
    public async Task<Guid> CreateVehicleModel(VehicleModelRequest entity) =>
        await repository.CreateAsync(entity.ToDomain());

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
    public async Task<VehicleModel?> UpdateVehicleModel(Guid id, VehicleModelRequest entity) =>
        await repository.UpdateAsync(id, entity.ToDomain());

    /// <summary>
    /// Deletes a vehicle model from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public async Task<bool> DeleteVehicleModel(Guid id) =>
        await repository.DeleteAsync(id);
}
