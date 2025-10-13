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
    public Guid CreateVehicleModel(VehicleModelDto entity) =>
        repository.Create(MapDto(entity));

    /// <summary>
    /// Retrieves all vehicle models.
    /// </summary>
    /// <returns>List of all vehicle models</returns>
    public List<VehicleModel> GetVehicleModels() =>
        repository.Read();

    /// <summary>
    /// Retrieves a specific vehicle model by ID.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    /// <returns>Vehicle model if found; otherwise null</returns>
    public VehicleModel? GetVehicleModel(Guid id) =>
        repository.Read(id);

    /// <summary>
    /// Updates an existing vehicle model.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    /// <param name="entity">Updated vehicle model data</param>
    /// <returns>Updated vehicle model if found; otherwise null</returns>
    public VehicleModel? UpdateVehicleModel(Guid id, VehicleModelDto entity) =>
        repository.Update(id, MapDto(entity));

    /// <summary>
    /// Deletes a vehicle model by ID.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    /// <returns>True if deleted successfully; otherwise false</returns>
    public bool DeleteVehicleModel(Guid id) =>
        repository.Delete(id);
}