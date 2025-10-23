using CarRentalService.Core.Domain.Models;

namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Represents a specific generation of a vehicle model in the car rental system.
/// </summary>
/// <param name="Id"> Id of model generation. </param>
/// <param name="Year"> Manufacturing year of the vehicle model generation. </param>
/// <param name="EngineVolume"> Engine volume in liters. </param>
/// <param name="TransmissionType"> Transmission type (e.g., Automatic, Manual). </param>
/// <param name="VehicleModelId"> Vehicle model Id </param>
/// <param name="RentalPricePerHour"> Cost to rent the vehicle per hour. </param>
public sealed record ModelGenerationDto(Guid Id, int Year, double EngineVolume, TransmissionType TransmissionType, Guid VehicleModelId, decimal RentalPricePerHour);
