namespace CarRentalService.Core.Domain.Models;

public class VehicleModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string DriveType { get; set; }
    public required int SeatCount { get; set; }
    public required string BodyType { get; set; }
    public required string Class { get; set; }
}
