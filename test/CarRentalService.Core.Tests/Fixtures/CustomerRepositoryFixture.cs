using CarRentalService.Application.Services;
using CarRentalService.Infrastructure.InMemory.Repositories;
using CarRentalService.Infrastructure.InMemory.Seeders;

namespace CarRentalService.Core.Tests.Fixtures;

/// <summary>
/// Provides test fixture for customer-related unit tests with pre-configured dependencies.
/// </summary>
public class CustomerRepositoryFixture
{
    /// <summary>
    /// Gets the customer service instance configured with in-memory repository for testing.
    /// </summary>
    /// <value>
    /// A <see cref="CustomerService"/> instance ready for use in unit tests.
    /// </value>
    public CustomerService CustomerService { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerRepositoryFixture"/> class.
    /// </summary>
    /// <remarks>
    /// The constructor sets up the complete dependency chain:
    /// <see cref="CustomerSeeder"/> → <see cref="InMemoryCustomerRepository"/> → <see cref="CustomerService"/>
    /// </remarks>
    public CustomerRepositoryFixture()
    {
        var customerSeeder = new CustomerSeeder();
        var customerRepository = new InMemoryCustomerRepository(customerSeeder);
        CustomerService = new CustomerService(customerRepository);
    }
}