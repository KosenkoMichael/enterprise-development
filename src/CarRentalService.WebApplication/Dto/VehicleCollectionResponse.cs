namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Vehicle collection response
/// </summary>
/// <param name="Vehicles">Collection of vehicles</param>
public sealed record VehicleCollectionResponse(IEnumerable<VehicleDto> Vehicles);
