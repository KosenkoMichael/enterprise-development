namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Vehicle request
/// </summary>
/// <param name="ModelGenerationId">Model generation id</param>
/// <param name="LicensePlate">License plate</param>
/// <param name="Color">Color</param>
public sealed record VehicleRequest(Guid ModelGenerationId, string LicensePlate, string Color);
