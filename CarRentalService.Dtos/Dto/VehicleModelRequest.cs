using CarRentalService.Core.Domain.Models;
using DriveType = CarRentalService.Core.Domain.Models.DriveType;

namespace CarRentalService.Dtos.Dto;

/// <summary>
/// Represents a vehicle model in the car rental system.
/// </summary>
public class VehicleModelRequest
{

    /// <summary>
    /// The name of the vehicle model.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The drive type (e.g., front-wheel, rear-wheel, all-wheel) of the vehicle model.
    /// </summary>
    public required DriveType DriveType { get; set; }


    /// <summary>
    /// The number of seats in the vehicle model.
    /// </summary>
    public required int SeatCount { get; set; }

    /// <summary>
    /// The body type (e.g., sedan, SUV, hatchback) of the vehicle model.
    /// </summary>
    public required BodyType BodyType { get; set; }

    /// <summary>
    /// The vehicle class (e.g., economy, luxury, premium) of the vehicle model.
    /// </summary>
    public required VehicleClass Class { get; set; }
}

