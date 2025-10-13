using CarRentalService.Core.Domain.Models;

namespace CarRentalService.Application.Dto;
public class ModelGenerationDto
{
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
    /// ID of model associated with this generation.
    /// </summary>
    public required Guid VehicleModelId { get; set; }
    /// <summary>
    /// Cost to rent the vehicle per hour.
    /// </summary>
    public required decimal RentalPricePerHour { get; set; }
}
