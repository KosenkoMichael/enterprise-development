namespace CarRentalService.Core.Domain.Models;
public class Rental
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Vehicle RentedVehicle { get; set; }
    public required Customer Customer { get; set; }
    public required DateTime RentStartTime { get; set; }
    public required double RentalDurationHours { get; set; }
    public decimal TotalCost =>
    (decimal)RentalDurationHours * RentedVehicle.Generation.RentalPricePerHour;
}