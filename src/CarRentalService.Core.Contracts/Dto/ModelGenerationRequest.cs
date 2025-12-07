using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Core.Contracts.Dto;

/// <summary>
/// Model generation request
/// </summary>
/// <param name="Year">Manufacture year</param>
/// <param name="EngineVolume">Engine volume</param>
/// <param name="TransmissionType">Transmission type</param>
/// <param name="VehicleModelId">Vehicle model id</param>
/// <param name="RentalPricePerHour">Rental price per hour</param>
public sealed record ModelGenerationRequest(int Year, double EngineVolume, TransmissionType TransmissionType, Guid VehicleModelId, decimal RentalPricePerHour);
