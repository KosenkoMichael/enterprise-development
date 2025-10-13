using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.TestData;

namespace CarRentalService.Infrastructure.InMemory.Seeders;

/// <summary>
/// Provides pre-generated customer data for testing.
/// </summary>
public class CustomerSeeder
{
    private readonly TestDataGenerator _generator = new();

    /// <summary>
    /// Returns a list of pre-generated customers.
    /// </summary>
    public List<Customer> GetItems()
    {
        var (_, _, _, customers, _) = _generator.GenerateTestData();
        return customers;
    }
}