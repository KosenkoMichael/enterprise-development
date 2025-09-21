namespace CarRentalService.Core.Domain.Models;
/// <summary>
/// Represents a vehicle model in the car rental system.
/// </summary>
public class VehicleModel
{
    /// <summary>
    /// Unique identifier for the vehicle model.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Vehicle model name (e.g., "Toyota Camry").
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Drive type of the vehicle (e.g., "FWD", "RWD", "AWD").
    /// </summary>
    public required DriveType DriveType { get; set; }
    /// <summary>
    /// Seat count of the vehicle.
    /// </summary>
    public required int SeatCount { get; set; }
    /// <summary>
    /// Body type of the vehicle (e.g., "Sedan", "SUV").
    /// </summary>
    public required BodyType BodyType { get; set; }
    /// <summary>
    /// Vehicle class (e.g., "Economy", "Luxury").
    /// </summary>
    public required VehicleClass Class { get; set; }
}
