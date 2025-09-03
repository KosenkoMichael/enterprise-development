namespace CarRentalService.Core.Domain.Models;
public class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required ModelGeneration Generation { get; set; }
    public required string LicensePlate { get; set; }
    public required string Color { get; set; }
}