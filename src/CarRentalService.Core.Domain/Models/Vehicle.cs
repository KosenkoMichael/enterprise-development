using System.Drawing;

namespace CarRentalService.Core.Domain.Models;
/// <summary>
/// Represents a vehicle in the car rental system.
/// </summary>
public class Vehicle
{
    /// <summary>
    /// Unique identifier for the vehicle.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Generation associated with this Vehicle.
    /// </summary>
    public required ModelGeneration Generation { get; set; }
    /// <summary>
    /// License plate number of the vehicle.
    /// </summary>
    public required string LicensePlate { get; set; }
    /// <summary>
    /// Color of the vehicle.
    /// </summary>
    public required Color Color { get; set; }
}