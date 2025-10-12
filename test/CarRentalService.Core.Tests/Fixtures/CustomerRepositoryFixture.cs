using CarRentalService.Application.Services;
using CarRentalService.Infrastructure.InMemory.Repositories;
using CarRentalService.Infrastructure.InMemory.Seeders;

namespace CarRentalService.Core.Tests.Fixtures;
/// <summary>
/// Fixture for Customer Unit tests
/// </summary>
public class CustomerRepositoryFixture
{
    /// <summary>
    /// Customer service instance
    /// </summary>
    public CustomerService CustomerService { get; }
    /// <summary>
    /// Fixture constructor
    /// </summary>
    public CustomerRepositoryFixture()
    {
        var customerSeeder = new CustomerSeeder();
        var customerRepository = new InMemoryCustomerRepository(customerSeeder);
        CustomerService = new CustomerService(customerRepository);
    }
}