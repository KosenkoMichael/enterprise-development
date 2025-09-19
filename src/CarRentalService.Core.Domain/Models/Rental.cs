namespace CarRentalService.Core.Domain.Models;
/// <summary>
/// Represents a rental transaction in the car rental system.
/// </summary>
public class Rental
{
    /// <summary>
    /// Unique identifier for the rental transaction.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Rented vehicle associated with this rental.
    /// </summary>
    public required Vehicle RentedVehicle { get; set; }
    /// <summary>
    /// Customer associated with this rental.
    /// </summary>
    public required Customer Customer { get; set; }
    /// <summary>
    /// Start time of the rental period.
    /// </summary>
    public required DateTime RentStartTime { get; set; }
    /// <summary>
    /// Duration of the rental in hours.
    /// </summary>
    public required double RentalDurationHours { get; set; }
    /// <summary>
    /// Total cost of the rental, calculated based on duration and vehicle's rental price per hour.
    /// </summary>
    public decimal TotalCost =>
    (decimal)RentalDurationHours * RentedVehicle.Generation.RentalPricePerHour;
}