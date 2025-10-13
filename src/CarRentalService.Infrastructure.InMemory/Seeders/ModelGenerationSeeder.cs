using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.TestData;

namespace CarRentalService.Infrastructure.InMemory.Seeders;

/// <summary>
/// Provides pre-generated model generation data for testing.
/// </summary>
public class ModelGenerationSeeder
{
    private readonly TestDataGenerator _generator = new();

    /// <summary>
    /// Returns a list of pre-generated model generations.
    /// </summary>
    public List<ModelGeneration> GetItems()
    {
        var (_, modelGenerations, _, _, _) = _generator.GenerateTestData();
        return modelGenerations;
    }
}