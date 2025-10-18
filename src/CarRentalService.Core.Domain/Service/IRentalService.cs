using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Core.Domain.Service;
/// <summary>
/// Interface for rental service
/// </summary>
public interface IRentalService
{
    /// <summary>
    /// Creates a new rental transaction.
    /// </summary>
    /// <param name="entity">The rental information.</param>
    /// <returns>The unique identifier of the newly created rental.</returns>
    public Task<Guid> CreateRentalAsync(Rental entity);
    /// <summary>
    /// Deletes a rental transaction from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public Task<bool> DeleteRentalAsync(Guid id);
    /// <summary>
    /// Retrieves a specific rental transaction by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the rental.</param>
    /// <returns>The rental if found; otherwise, null.</returns>
    public Task<Rental?> GetRentalAsync(Guid id);
    /// <summary>
    /// Retrieves all rental transactions from the system.
    /// </summary>
    /// <returns>A list of all rentals.</returns>
    public Task<List<Rental>> GetRentalsAsync();
    /// <summary>
    /// Updates an existing rental transaction.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to update.</param>
    /// <param name="entity">The updated rental information.</param>
    /// <returns>The updated rental if successful; otherwise, null.</returns>
    public Task<Rental?> UpdateRentalAsync(Guid id, Rental entity);
}