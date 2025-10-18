namespace CarRentalService.Dtos.Dto;

/// <summary>
/// Collection for top N vehicles
/// </summary>
/// <param name="VehicleRentalCount">vehicle and rentalcount</param>
public sealed record VehicleRentalCountCollectionResponse(IEnumerable<VehicleRentalCountDto> VehicleRentalCount);
