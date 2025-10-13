using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.TestData;

namespace CarRentalService.Infrastructure.InMemory.Seeders;

/// <summary>
/// Provides pre-generated vehicle model data for testing.
/// </summary>
public class VehicleModelSeeder
{
    private readonly TestDataGenerator _generator = new();

    /// <summary>
    /// Returns a list of pre-generated vehicle models.
    /// </summary>
    public List<VehicleModel> GetItems()
    {
        var (vehicleModels, _, _, _, _) = _generator.GenerateTestData();
        return vehicleModels;
    }
}