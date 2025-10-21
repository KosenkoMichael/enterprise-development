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

    /// <summary>
    /// Navigation property to model generation
    /// </summary>
    public ModelGeneration ModelGeneration { get; set; } = null!;

    /// <summary>
    /// Navigation property to rentals
    /// </summary>
    public ICollection<Rental> Rentals { get; set; } = null!;
}