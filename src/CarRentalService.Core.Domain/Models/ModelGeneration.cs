namespace CarRentalService.Core.Domain.Models;
public class ModelGeneration
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required int Year { get; set; }
    public required double EngineVolume { get; set; }
    public required string TransmissionType { get; set; }
    public required VehicleModel Model { get; set; }
    public required decimal RentalPricePerHour { get; set; }
}
