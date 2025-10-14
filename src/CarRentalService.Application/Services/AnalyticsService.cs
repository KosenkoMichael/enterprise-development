using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service providing advanced analytics for rentals, vehicles, and customers.
/// </summary>
public class AnalyticsService(
        IRepository<Rental> _rentalRepository,
        IRepository<Vehicle> _vehicleRepository,
        IRepository<Customer> _customerRepository,
        IRepository<ModelGeneration> _modelGenerationRepository)
{
    /// <summary>
    /// Returns all customers who rented vehicles of a specified model, ordered by full name.
    /// </summary>
    public async Task<List<Customer>> GetCustomersByVehicleModelAsync(Guid vehicleModelId)
    {
        var rentals = await _rentalRepository.ReadAllAsync();
        var vehicles = await _vehicleRepository.ReadAllAsync();
        var customers = await _customerRepository.ReadAllAsync();
        var modelGenerations = await _modelGenerationRepository.ReadAllAsync();

        var result = rentals
            .Where(r =>
            {
                var vehicle = vehicles.FirstOrDefault(v => v.Id == r.VehicleId);
                if (vehicle == null) return false;
                var modelGen = modelGenerations.FirstOrDefault(mg => mg.Id == vehicle.ModelGenerationId);
                return modelGen != null && modelGen.VehicleModelId == vehicleModelId;
            })
            .Select(r => customers.First(c => c.Id == r.CustomerId))
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns all vehicles that are currently rented.
    /// </summary>
    public async Task<List<Vehicle>> GetVehiclesCurrentlyRentedAsync()
    {
        var rentals = await _rentalRepository.ReadAllAsync();
        var vehicles = await _vehicleRepository.ReadAllAsync();

        var now = DateTime.Now;
        var rentedVehicleIds = rentals
            .Where(r => r.RentStartTime <= now && r.RentStartTime.AddHours(r.RentalDurationHours) >= now)
            .Select(r => r.VehicleId)
            .Distinct();

        return vehicles.Where(v => rentedVehicleIds.Contains(v.Id)).ToList();
    }

    /// <summary>
    /// Returns top 5 most frequently rented vehicles.
    /// </summary>
    public async Task<List<(Vehicle Vehicle, int RentalCount)>> GetTopRentedVehiclesAsync(int top = 5)
    {
        var rentals = await _rentalRepository.ReadAllAsync();
        var vehicles = await _vehicleRepository.ReadAllAsync();

        var result = rentals
            .GroupBy(r => r.VehicleId)
            .Select(g => (Vehicle: vehicles.First(v => v.Id == g.Key), RentalCount: g.Count()))
            .OrderByDescending(x => x.RentalCount)
            .Take(top)
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns the number of rentals for each vehicle.
    /// </summary>
    public async Task<List<(Vehicle Vehicle, int RentalCount)>> GetRentalCountPerVehicleAsync()
    {
        var rentals = await _rentalRepository.ReadAllAsync();
        var vehicles = await _vehicleRepository.ReadAllAsync();

        var result = rentals
            .GroupBy(r => r.VehicleId)
            .Select(g => (Vehicle: vehicles.First(v => v.Id == g.Key), RentalCount: g.Count()))
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns top 5 customers by total amount spent on rentals.
    /// </summary>
    public async Task<List<(Customer Customer, decimal TotalSpent)>> GetTopCustomersByRentalSumAsync(int top = 5)
    {
        var rentals = await _rentalRepository.ReadAllAsync();
        var customers = await _customerRepository.ReadAllAsync();
        var vehicles = await _vehicleRepository.ReadAllAsync();
        var modelGenerations = await _modelGenerationRepository.ReadAllAsync();

        var result = rentals
            .GroupBy(r => r.CustomerId)
            .Select(g =>
            {
                var total = g.Sum(r =>
                {
                    var vehicle = vehicles.First(v => v.Id == r.VehicleId);
                    var modelGen = modelGenerations.First(mg => mg.Id == vehicle.ModelGenerationId);
                    return (decimal)r.RentalDurationHours * modelGen.RentalPricePerHour;
                });
                return (Customer: customers.First(c => c.Id == g.Key), TotalSpent: total);
            })
            .OrderByDescending(x => x.TotalSpent)
            .Take(top)
            .ToList();

        return result;
    }
}
