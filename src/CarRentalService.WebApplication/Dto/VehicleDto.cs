namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Represents a vehicle in the car rental system.
/// </summary>
/// <param name="Id"> Unique identifier for the vehicle. </param>
/// <param name="ModelGenerationId"> Generation ID associated with this Vehicle. </param>
/// <param name="LicensePlate"> License plate number of the vehicle. </param>
/// <param name="Color"> Color of the vehicle. </param>
public sealed record VehicleDto(Guid Id, Guid ModelGenerationId, string LicensePlate, string Color);
