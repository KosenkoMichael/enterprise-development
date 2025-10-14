namespace CarRentalService.Application.Dto;
public class RentalDto
{
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
}
