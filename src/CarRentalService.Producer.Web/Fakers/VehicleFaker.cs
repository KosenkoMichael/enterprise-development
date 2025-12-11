using Bogus;
using CarRentalService.Core.Contracts.Dto;
using System.Drawing;

namespace CarRentalService.Producer.Web.Fakers;

/// <summary>
/// Faker for vehicle
/// </summary>
public static class VehicleFaker
{
    /// <summary>
    /// Generate N vehicles
    /// </summary>
    /// <param name="count">count</param>
    /// <param name="modelGenerationIds">List of model generations ids</param>
    /// <returns>List of vehicles</returns>
    /// <exception cref="ArgumentException">throw if list is empty</exception>
    public static List<VehicleRequest> Generate(int count, List<Guid> modelGenerationIds)
    {
        if (modelGenerationIds == null || modelGenerationIds.Count == 0)
            throw new ArgumentException("ModelGenerationIds cannot be empty.");

        var goodColors = Enum.GetValues<KnownColor>()
            .Where(c => !Color.FromKnownColor(c).IsSystemColor)
            .ToList();

        var faker = new Faker<VehicleRequest>()
            .CustomInstantiator(f => new VehicleRequest(
                ModelGenerationId: f.PickRandom(modelGenerationIds),
                LicensePlate: $"{f.Random.String2(1)}{f.Random.Number(000, 999):000}{f.Random.String2(2)}",
                Color: Color.FromKnownColor(f.PickRandom(goodColors)).Name
            ));

        return faker.Generate(count);
    }
}
