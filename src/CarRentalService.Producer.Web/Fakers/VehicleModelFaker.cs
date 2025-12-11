using Bogus;
using CarRentalService.Core.Contracts.Dto;
using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Producer.Web.Fakers;

/// <summary>
/// Faker for vehicle model
/// </summary>
public static class VehicleModelFaker
{
    private static readonly Faker<VehicleModelRequest> _faker = new Faker<VehicleModelRequest>()
        .CustomInstantiator(f => new VehicleModelRequest(
            Name: f.Vehicle.Manufacturer(),
            DriveType: f.PickRandom<Core.Domain.Models.DriveType>(),
            SeatCount: f.Random.Int(1, 4),
            BodyType: f.PickRandom<BodyType>(),
            Class: f.PickRandom<VehicleClass>()
        ));

    /// <summary>
    /// Generate N random VehicleModelRequests
    /// </summary>
    /// <param name="count">count</param>
    /// <returns>List of randomly generated VehicleModelRequests </returns>
    public static List<VehicleModelRequest> Generate(int count)
        => _faker.Generate(count);
}
