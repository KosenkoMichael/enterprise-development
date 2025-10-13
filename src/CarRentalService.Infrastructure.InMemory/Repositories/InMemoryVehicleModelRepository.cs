using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.Infrastructure.InMemory.Seeders;

namespace CarRentalService.Infrastructure.InMemory.Repositories;
/// <summary>
/// In-memory implementation of the vehicle model repository
/// </summary>
public class InMemoryVehicleModelRepository: IRepository<VehicleModel>
{
    private readonly List<VehicleModel> _vehicleModels = new();
    /// <summary>
    /// Initializes a new instance of the vehicle model repository.
    /// </summary>
    /// <param name="seeder">Seeder for initial data</param>
    public InMemoryVehicleModelRepository(VehicleModelSeeder? seeder)
    {
        if (seeder == null) return;
        _vehicleModels = seeder.GetItems();
    }
    /// <inheritdoc/>
    public Guid Create(VehicleModel entity)
    {
        _vehicleModels.Add(entity);
        return entity.Id;
    }
    /// <inheritdoc/>
    public bool Delete(Guid id)
    {
        var vehicleModel = _vehicleModels.FirstOrDefault(vm => vm.Id == id);
        if (vehicleModel == null)
        {
            return false;
        }
        _vehicleModels.Remove(vehicleModel);
        return true;
    }
    /// <inheritdoc/>
    public List<VehicleModel> Read() =>
        [.. _vehicleModels];
    /// <inheritdoc/>
    public VehicleModel? Read(Guid id) =>
        _vehicleModels.FirstOrDefault(vm => vm.Id == id);
    /// <inheritdoc/>
    public VehicleModel? Update(Guid id, VehicleModel entity)
    {
        var index = _vehicleModels.FindIndex(vm => vm.Id == id);
        if (index == -1)
        {
            return null;
        }
        entity.Id = id;
        _vehicleModels[index] = entity;
        return entity;
    }
}
