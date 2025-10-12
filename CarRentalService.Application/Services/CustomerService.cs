using CarRentalService.Application.Dto;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;

namespace CarRentalService.Application.Services;
/// <summary>
/// Service for managing customers
/// </summary>
/// <param name="repository"></param>
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
    /// Creates a new customer
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Guid CreateStudent(CustomerDto entity)
    {
        return repository.Create(MapDto(entity));
    }
    /// <summary>
    /// Retrieves all customers
    /// </summary>
    /// <returns></returns>
    public List<Customer> GetCustomers()
    {
        return repository.Read();
    }
    /// <summary>
    /// Retrieves a customer by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Customer? GetCustomer(Guid id)
    {
        return repository.Read(id);
    }
    /// <summary>
    /// Updates an existing customer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Customer? UpdateCustomer(Guid id, CustomerDto entity)
    {
        return repository.Update(id, MapDto(entity));
    }
    /// <summary>
    /// Deletes a customer by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool DeleteCustomer(Guid id)
    {
        return repository.Delete(id);
    }
}
