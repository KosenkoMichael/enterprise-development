using CarRentalService.WebApplication.Dto;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.WebApplication.Mappers;
using CarRentalService.Core.Domain.Service;

namespace CarRentalService.WebApplication.Services;

/// <summary>
/// Provides operations for managing customers in the car rental system.
/// </summary>
/// <param name="repository">The repository used for customer data access.</param>
public class CustomerService(IRepository<Customer> repository) : ICustomerService
{
    /// <summary>
    /// Creates a new customer in the system.
    /// </summary>
    /// <param name="entity">The data transfer object containing customer information.</param>
    /// <returns>The unique identifier of the newly created customer.</returns>
    public async Task<Guid> CreateCustomerAsync(Customer entity) =>
        await repository.CreateAsync(entity);

    /// <summary>
    /// Retrieves all customers from the system.
    /// </summary>
    /// <returns>A list of all customers.</returns>
    public async Task<List<Customer>> GetCustomersAsync() =>
        await repository.ReadAllAsync();

    /// <summary>
    /// Retrieves a specific customer by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <returns>The customer if found; otherwise, null.</returns>
    public async Task<Customer?> GetCustomerAsync(Guid id) =>
        await repository.ReadAsync(id);

    /// <summary>
    /// Updates an existing customer's information.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to update.</param>
    /// <param name="entity">The data transfer object containing updated customer information.</param>
    /// <returns>The updated customer if successful; otherwise, null.</returns>
    public async Task<Customer?> UpdateCustomerAsync(Guid id, Customer entity) =>
        await repository.UpdateAsync(id, entity);

    /// <summary>
    /// Deletes a customer from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public async Task<bool> DeleteCustomerAsync(Guid id) =>
        await repository.DeleteAsync(id);
}