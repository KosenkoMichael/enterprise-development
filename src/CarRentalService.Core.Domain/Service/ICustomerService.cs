using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Core.Domain.Service;

/// <summary>
/// Interface for customer service
/// </summary>
public interface ICustomerService
{

    /// <summary>
    /// Creates a new customer in the system.
    /// </summary>
    /// <param name="entity">The data transfer object containing customer information.</param>
    /// <returns>The unique identifier of the newly created customer.</returns>
    public Task<Guid> CreateCustomerAsync(Customer entity);

    /// <summary>
    /// Deletes a customer from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public Task<bool> DeleteCustomerAsync(Guid id);

    /// <summary>
    /// Retrieves a specific customer by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <returns>The customer if found; otherwise, null.</returns>
    public Task<Customer?> GetCustomerAsync(Guid id);

    /// <summary>
    /// Retrieves all customers from the system.
    /// </summary>
    /// <returns>A list of all customers.</returns>
    public Task<List<Customer>> GetCustomersAsync();

    /// <summary>
    /// Updates an existing customer's information.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to update.</param>
    /// <param name="entity">The data transfer object containing updated customer information.</param>
    /// <returns>The updated customer if successful; otherwise, null.</returns>
    public Task<Customer?> UpdateCustomerAsync(Guid id, Customer entity);
}