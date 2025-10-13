using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Tests.Fixtures;
using CarRentalService.Application.Services;

namespace CarRentalService.Core.Tests;

/// <summary>
/// Unit tests for customer management functionality.
/// </summary>
public class CustomerTests(CustomerRepositoryFixture fixture) : IClassFixture<CustomerRepositoryFixture>
{
    /// <summary>
    /// Finds customer by full name and verifies driver license number.
    /// </summary>
    [Fact(DisplayName = "Should find customer by FullName and verify DriverLicenseNumber")]
    public void Test_FindCustomerByFullNameAndCheckLicense()
    {
        var customer = (from c in fixture.CustomerService.GetCustomers()
                        where c.FullName == "Smith John"
                        select c).FirstOrDefault();
        Assert.NotNull(customer);
        Assert.Equal("77AB100001", customer.DriverLicenseNumber);
    }
}