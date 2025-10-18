namespace CarRentalService.Dtos.Dto;

/// <summary>
/// Rental collection response
/// </summary>
/// <param name="Rentals">Collection of rentals</param>
public sealed record RentalCollectionResponse(IEnumerable<RentalDto> Rentals);
