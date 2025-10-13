namespace CarRentalService.Core.Domain.Models;
/// <summary>
/// Represents a specific generation of a vehicle model in the car rental system.
/// </summary>
public class ModelGeneration
{
    /// <summary>
    /// Unique identifier for the model generation.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Manufacturing year of the vehicle model generation.
    /// </summary>
    public required int Year { get; set; }
    /// <summary>
    /// Engine volume in liters.
    /// </summary>
    public required double EngineVolume { get; set; }
    /// <summary>
    /// Transmission type (e.g., Automatic, Manual).
    /// </summary>
    public required TransmissionType TransmissionType { get; set; }
    /// <summary>
    /// Vehicle model Id
    /// </summary>
    public required Guid VehicleModelId { get; set; }
    /// <summary>
    /// Cost to rent the vehicle per hour.
    /// </summary>
    public required decimal RentalPricePerHour { get; set; }
}
