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
    /// Id of rented vehicle associated with this rental.
    /// </summary>
    public required Guid VehicleId { get; set; }
    /// <summary>
    /// Id of customer associated with this rental.
    /// </summary>
    public required Guid CustomerId { get; set; }
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
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Navigation property to customer
    /// </summary>
    public Customer Customer { get; set; } = null!;

    /// <summary>
    /// Navigation property to vehicle
    /// </summary>
    public Vehicle Vehicle { get; set; } = null!;
}