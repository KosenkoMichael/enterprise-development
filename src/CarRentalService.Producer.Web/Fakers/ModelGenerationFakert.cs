using Bogus;
using CarRentalService.Core.Contracts.Dto;
using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Producer.Web.Fakers;

/// <summary>
/// Faker for model generation
/// </summary>
public static class ModelGenerationFaker
{
    /// <summary>
    /// Generate N model generations
    /// </summary>
    /// <param name="count">count</param>
    /// <param name="vehicleModelIds">List of vehicle moidel ids</param>
    /// <returns>List of model generations</returns>
    /// <exception cref="ArgumentException">throw if list is empty</exception>
    public static List<ModelGenerationRequest> Generate(int count, List<Guid> vehicleModelIds)
    {
        if (vehicleModelIds == null || vehicleModelIds.Count == 0)
            throw new ArgumentException("VehicleModelIds cannot be empty.");

        var faker = new Faker<ModelGenerationRequest>()
            .CustomInstantiator(f => new ModelGenerationRequest(
                Year: f.Random.Int(2000, 2025),
                EngineVolume: Math.Round(f.Random.Double(1.0, 5.0), 1),
                TransmissionType: f.PickRandom<TransmissionType>(),
                VehicleModelId: f.PickRandom(vehicleModelIds),
                RentalPricePerHour: f.Random.Decimal(10, 500)
            ));

        return faker.Generate(count);
    }
}
