using System.Drawing;

namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Represents a vehicle in the car rental system.
/// </summary>
public class VehicleDto
{

    /// <summary>
    /// The unique identifier of the model generation this vehicle belongs to.
    /// </summary>
    public required Guid ModelGenerationId { get; set; }

    /// <summary>
    /// The license plate number of the vehicle.
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// The color of the vehicle.
    /// </summary>
    public required string Color { get; set; }
}
