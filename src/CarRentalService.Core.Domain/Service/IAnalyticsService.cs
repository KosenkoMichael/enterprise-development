using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Core.Domain.Service;

/// <summary>
/// Interafce for analytics service
/// </summary>
public interface IAnalyticsService
{

    /// <summary>
    /// Returns all customers who rented vehicles of a specified model, ordered by full name.
    /// </summary>
    public Task<List<Customer>> GetCustomersByVehicleModelAsync(Guid vehicleModelId);

    /// <summary>
    /// Returns the number of rentals for each vehicle.
    /// </summary>
    public Task<List<(Vehicle Vehicle, int RentalCount)>> GetRentalCountPerVehicleAsync();

    /// <summary>
    /// Returns top 5 customers by total amount spent on rentals.
    /// </summary>
    public Task<List<(Customer Customer, decimal TotalSpent)>> GetTopCustomersByRentalSumAsync(int top = 5);

    /// <summary>
    /// Returns top 5 most frequently rented vehicles.
    /// </summary>
    public Task<List<(Vehicle Vehicle, int RentalCount)>> GetTopRentedVehiclesAsync(int top = 5);

    /// <summary>
    /// Returns all vehicles that are currently rented.
    /// </summary>
    public Task<List<Vehicle>> GetVehiclesCurrentlyRentedAsync();
}