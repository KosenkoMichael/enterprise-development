using CarRentalService.Application.Services;
namespace CarRentalService.Core.Tests.Fixtures;

/// <summary>
/// 
/// </summary>
public class CustomerRepositoryFixture
{
    /// <summary>
    /// 
    /// </summary>
    public CustomerService CustomerService { get; }

    /// <summary>
    /// 
    /// </summary>
    public CustomerRepositoryFixture()
    {
        var customerSeeder = new CustomerSeeder();
        var customerRepository = new InMemoryCustomerRepository(customerSeeder);
        CustomerService = new CustomerService(customerRepository);
    }
}