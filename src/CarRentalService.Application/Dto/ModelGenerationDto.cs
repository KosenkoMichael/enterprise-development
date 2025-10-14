using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Application.Dto;

/// <summary>
/// Represents a specific generation of a vehicle model in the car rental system.
/// </summary>
public class ModelGenerationDto
{

    /// <summary>
    /// The year this model generation was released.
    /// </summary>
    public required int Year { get; set; }

    /// <summary>
    /// The engine volume (in liters) of this model generation.
    /// </summary>
    public required double EngineVolume { get; set; }

    /// <summary>
    /// The type of transmission for this model generation.
    /// </summary>
    public required TransmissionType TransmissionType { get; set; }

    /// <summary>
    /// The unique identifier of the vehicle model this generation belongs to.
    /// </summary>
    public required Guid VehicleModelId { get; set; }

    /// <summary>
    /// The rental price per hour for this model generation.
    /// </summary>
    public required decimal RentalPricePerHour { get; set; }
}
