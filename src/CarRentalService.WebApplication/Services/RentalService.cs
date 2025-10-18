using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.Core.Domain.Service;
using CarRentalService.Infrastructure.Repositories;

namespace CarRentalService.WebApplication.Services;

/// <summary>
/// Provides operations for managing rentals in the car rental system.
/// </summary>
/// <param name="rentalRepository">Repository for accessing rental data.</param>
/// <param name="customerRepository">Repository for validating and accessing customer data.</param>
/// <param name="vehicleRepository">Repository for validating and accessing vehicle data.</param>
public class RentalService(
    IRepository<Rental> rentalRepository,
    IRepository<Customer> customerRepository,
    IRepository<Vehicle> vehicleRepository
    ) : IRentalService
{
    /// <summary>
    /// Creates a new rental transaction.
    /// </summary>
    /// <param name="entity">The rental information.</param>
    /// <returns>The unique identifier of the newly created rental.</returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown if the specified customer or vehicle does not exist.
    /// </exception>
    public async Task<Guid> CreateRentalAsync(Rental entity)
    {
        if (await customerRepository.ReadAsync(entity.CustomerId) is null)
        {
            throw new KeyNotFoundException($"Customer with Id = {entity.CustomerId} does not found");
        }

        if (await vehicleRepository.ReadAsync(entity.VehicleId) is null)
        {
            throw new KeyNotFoundException($"Vehicle with Id = {entity.VehicleId} does not found");
        }

        return await rentalRepository.CreateAsync(entity);
    }

    /// <summary>
    /// Retrieves all rental transactions from the system.
    /// </summary>
    /// <returns>A list of all rentals.</returns>
    public async Task<List<Rental>> GetRentalsAsync() =>
        await rentalRepository.ReadAllAsync();

    /// <summary>
    /// Retrieves a specific rental transaction by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the rental.</param>
    /// <returns>The rental if found; otherwise, null.</returns>
    public async Task<Rental?> GetRentalAsync(Guid id) =>
        await rentalRepository.ReadAsync(id);

    /// <summary>
    /// Updates an existing rental transaction.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to update.</param>
    /// <param name="entity">The updated rental information.</param>
    /// <returns>The updated rental if successful; otherwise, null.</returns>
    public async Task<Rental?> UpdateRentalAsync(Guid id, Rental entity) =>
        await rentalRepository.UpdateAsync(id, entity);

    /// <summary>
    /// Deletes a rental transaction from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public async Task<bool> DeleteRentalAsync(Guid id) =>
        await rentalRepository.DeleteAsync(id);

    /// <summary>
    /// Returns vehicle, related with this rental
    /// </summary>
    /// <param name="id">id of rental</param>
    /// <returns>vehicle</returns>
    public async Task<Vehicle?> GetRelatedVehicle(Guid id)
    {
        var rental = await rentalRepository.ReadAsync(id);
        return await vehicleRepository.ReadAsync(rental!.VehicleId);
    }

    /// <summary>
    /// Returns customer, related with this rental
    /// </summary>
    /// <param name="id">id of rental</param>
    /// <returns>customer</returns>
    public async Task<Customer?> GetRelatedCustomer(Guid id)
    {
        var rental = await rentalRepository.ReadAsync(id);
        return await customerRepository.ReadAsync(rental!.CustomerId);
    }
}
