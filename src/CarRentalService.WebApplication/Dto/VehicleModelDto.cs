using CarRentalService.Core.Domain.Models;
using DriveType = CarRentalService.Core.Domain.Models.DriveType;

namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Represents a vehicle model in the car rental system.
/// </summary>
/// <param name="Id"> Unique identifier for the vehicle model. </param>
/// <param name="Name"> Vehicle model name (e.g., "Toyota Camry"). </param>
/// <param name="DriveType"> Drive type of the vehicle (e.g., "FWD", "RWD", "AWD"). </param>
/// <param name="SeatCount"> Seat count of the vehicle. </param>
/// <param name="BodyType"> Body type of the vehicle (e.g., "Sedan", "SUV"). </param>
/// <param name="Class"> Vehicle class (e.g., "Economy", "Luxury"). </param>
public record VehicleModelDto(Guid Id, string Name, DriveType DriveType, int SeatCount, BodyType BodyType, VehicleClass Class);
