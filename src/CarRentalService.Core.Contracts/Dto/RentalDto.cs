namespace CarRentalService.Core.Contracts.Dto;

/// <summary>
/// Represents a rental transaction in the car rental system.
/// </summary>
/// <param name="Id"> Id of rental. </param>
/// <param name="VehicleId"> Id of rented vehicle associated with this rental. </param>
/// <param name="CustomerId"> Id of customer associated with this rental. </param>
/// <param name="RentStartTime"> Start time of the rental period. </param>
/// <param name="RentalDurationHours"> Duration of the rental in hours. </param>
/// <param name="TotalCost"> Total cost of the rental, calculated based on duration and vehicle's rental price per hour. </param>
public sealed record RentalDto(Guid Id, Guid VehicleId, Guid CustomerId, DateTime RentStartTime, double RentalDurationHours, decimal TotalCost);