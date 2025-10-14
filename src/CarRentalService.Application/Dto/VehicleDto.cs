using System.Drawing;

namespace CarRentalService.Application.Dto;
public class VehicleDto
{
    /// <summary>
    /// Generation ID associated with this Vehicle.
    /// </summary>
    public required Guid ModelGenerationId { get; set; }
    /// <summary>
    /// License plate number of the vehicle.
    /// </summary>
    public required string LicensePlate { get; set; }
    /// <summary>
    /// Color of the vehicle.
    /// </summary>
    public required string Color { get; set; }
}
