using Bogus;
using CarRentalService.Core.Contracts.Dto;

namespace CarRentalService.Producer.Web.Fakers;

/// <summary>
/// Faker for rental
/// </summary>
public static class RentalFaker
{
    /// <summary>
    /// Generate N rentals
    /// </summary>
    /// <param name="count">count</param>
    /// <param name="vehicleIds">List of vehicle ids</param>
    /// <param name="customerIds">List of customer ids</param>
    /// <returns>List of rentals</returns>
    /// <exception cref="ArgumentException">throw if list is empty</exception>
    public static List<RentalRequest> Generate(int count, List<Guid> vehicleIds, List<Guid> customerIds)
    {
        if (vehicleIds.Count == 0)
            throw new ArgumentException("VehicleIds cannot be empty.");
        if (customerIds.Count == 0)
            throw new ArgumentException("CustomerIds cannot be empty.");

        var faker = new Faker<RentalRequest>()
            .CustomInstantiator(f => new RentalRequest(
                VehicleId: f.PickRandom(vehicleIds),
                CustomerId: f.PickRandom(customerIds),
                RentStartTime: f.Date.Past(60),
                RentalDurationHours: Math.Round(f.Random.Double(0.5, 12.0) * 2) / 2
            ));

        return faker.Generate(count);
    }
}
