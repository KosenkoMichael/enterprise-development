namespace CarRentalService.Core.Contracts.Dto;

/// <summary>
/// Dto for top N vehicles
/// </summary>
/// <param name="Vehicle">Vehicle</param>
/// <param name="RentalCount">Rental count</param>
public sealed record VehicleRentalCountDto(VehicleDto Vehicle, int RentalCount);
