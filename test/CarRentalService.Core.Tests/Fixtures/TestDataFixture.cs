using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.TestData;

namespace CarRentalService.Core.Tests.Fixtures;
/// <summary>
/// Fixture for Unit tests
/// </summary>
public class TestDataFixture
{
    /// <summary>
    /// Test List of VehicleModels
    /// </summary>
    public List<VehicleModel> VehicleModels { get; }
    /// <summary>
    /// Test List of ModelGenerations
    /// </summary>
    public List<ModelGeneration> ModelGenerations { get; }
    /// <summary>
    /// Test List of Vehicles
    /// </summary>
    public List<Vehicle> Vehicles { get; }
    /// <summary>
    /// Test List of Customers
    /// </summary>
    public List<Customer> Customers { get; }
    /// <summary>
    /// Test List of Rentals
    /// </summary>
    public List<Rental> Rentals { get; }
    /// <summary>
    /// Test data for Unit tests
    /// </summary>
    public TestDataFixture()
    {
        var generator = new TestDataGenerator();
        (VehicleModels, ModelGenerations, Vehicles, Customers, Rentals) = generator.GenerateTestData();
    }
}