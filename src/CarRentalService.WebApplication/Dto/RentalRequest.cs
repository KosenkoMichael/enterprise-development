namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Represents a rental transaction in the car rental system.
/// </summary>
public class RentalRequest
{

    /// <summary>
    /// The unique identifier of the rented vehicle.
    /// </summary>
    public required Guid VehicleId { get; set; }

    /// <summary>
    /// The unique identifier of the customer who rents the vehicle.
    /// </summary>
    public required Guid CustomerId { get; set; }

    /// <summary>
    /// The start time of the rental period.
    /// </summary>
    public required DateTime RentStartTime { get; set; }

    /// <summary>
    /// The duration of the rental in hours.
    /// </summary>
    public required double RentalDurationHours { get; set; }
}
