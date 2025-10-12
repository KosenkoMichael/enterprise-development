using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.TestData;

namespace CarRentalService.Infrastructure.InMemory.Seeders;
/// <summary>
/// Seeder for customers
/// </summary>
public class CustomerSeeder
{
    private readonly TestDataGenerator _generator = new();
    /// <summary>
    /// Pre-generated list of customers
    /// </summary>
    /// <returns></returns>
    public List<Customer> GetItems()
    {
        var (_, _, _, customers, _) = _generator.GenerateTestData();
        return customers;
    }
}
