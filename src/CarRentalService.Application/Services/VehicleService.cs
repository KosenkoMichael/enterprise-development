using CarRentalService.Application.Dto;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;

namespace CarRentalService.Application.Services;
public class VehicleService(
    IRepository<Vehicle> vehicleRepository,
    IRepository<ModelGeneration> modelGenerationRepository
    )
{
    private static Vehicle MapDto(VehicleDto entity)
    {
        return new Vehicle
        {
            ModelGenerationId = entity.ModelGenerationId,
            LicensePlate = entity.LicensePlate,
            Color = entity.Color
        };
    }

    /// <summary>
    /// Creates a new Vehicle
    /// </summary>
    /// <param name="entity">Vehicle data</param>
    /// <returns>Unique identifier of created Vehicle</returns>
    /// <exception cref="Exception"></exception>
    public async Task<Guid> CreateVehicleAsync(VehicleDto entity)
    {
        if ( await modelGenerationRepository.ReadAsync(entity.ModelGenerationId) is null)
        {
            throw new Exception($"ModelGeneration with Id = {entity.ModelGenerationId} does not found");
        }

        return await vehicleRepository.CreateAsync(MapDto(entity));
    }

    public async Task<List<Vehicle>> GetVehiclesAsync() =>
        await vehicleRepository.ReadAllAsync();

    public async Task<Vehicle?> GetVehicleAsync(Guid id) =>
        await vehicleRepository.ReadAsync(id);

    public async Task<Vehicle?> UpdateVehicleAsync(Guid id, VehicleDto entity) =>
        await vehicleRepository.UpdateAsync(id, MapDto(entity));
    public async Task<bool> DeleteVehicleAsync(Guid id) =>
        await vehicleRepository.DeleteAsync(id);
}
