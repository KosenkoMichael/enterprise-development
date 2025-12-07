namespace CarRentalService.Core.Contracts.Dto;

/// <summary>
/// Vehicle model collection response
/// </summary>
/// <param name="VehicleModels">Collection of vehicle models</param>
public sealed record VehicleModelCollectionResponse(IEnumerable<VehicleModelDto> VehicleModels);
