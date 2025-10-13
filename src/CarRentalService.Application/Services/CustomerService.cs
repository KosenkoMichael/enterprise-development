using CarRentalService.Application.Dto;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;

namespace CarRentalService.Application.Services;

/// <summary>
/// Provides services for managing customer entities in the system.
/// </summary>
/// <param name="repository">The repository used for customer data operations.</param>
public class CustomerService(IRepository<Customer> repository)
{
    private static Customer MapDto(CustomerDto entity)
    {
        return new Customer
        {
            DriverLicenseNumber = entity.DriverLicenseNumber,
            FullName = entity.FullName,
            DateOfBirth = entity.DateOfBirth
        };
    }

    /// <summary>
    /// Creates a new customer in the system.
    /// </summary>
    /// <param name="entity">The customer data to create.</param>
    /// <returns>The unique identifier of the newly created customer.</returns>
    public async Task<Guid> CreateCustomerAsync(CustomerDto entity) =>
        await repository.CreateAsync(MapDto(entity));

    /// <summary>
    /// Retrieves all customers from the system.
    /// </summary>
    /// <returns>A list of all customer entities.</returns>
    public async Task<List<Customer>> GetCustomersAsync() =>
        await repository.ReadAllAsync();

    /// <summary>
    /// Retrieves a specific customer by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <returns>The customer entity if found; otherwise, <see langword="null"/>.</returns>
    public async Task<Customer?> GetCustomerAsync(Guid id) =>
        await repository.ReadAsync(id);

    /// <summary>
    /// Updates an existing customer's information.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to update.</param>
    /// <param name="entity">The updated customer data.</param>
    /// <returns>The updated customer entity if found; otherwise, <see langword="null"/>.</returns>
    public async Task<Customer?> UpdateCustomerAsync(Guid id, CustomerDto entity) =>
        await repository.UpdateAsync(id, MapDto(entity));

    /// <summary>
    /// Deletes a customer from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to delete.</param>
    /// <returns><see langword="true"/> if the customer was successfully deleted; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> DeleteCustomerAsync(Guid id) =>
        await repository.DeleteAsync(id);
}