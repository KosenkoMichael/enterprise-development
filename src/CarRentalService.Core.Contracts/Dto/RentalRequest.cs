namespace CarRentalService.Core.Contracts.Dto;

/// <summary>
/// Rental request
/// </summary>
/// <param name="VehicleId">Vehicle id</param>
/// <param name="CustomerId">Customer id</param>
/// <param name="RentStartTime">Rent start time</param>
/// <param name="RentalDurationHours">Rental duration hours</param>
public sealed record RentalRequest(Guid VehicleId, Guid CustomerId, DateTime RentStartTime, double RentalDurationHours);
