namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Customer collection response
/// </summary>
/// <param name="Customers">Collection of customers</param>
public sealed record CustomerCollectionResponse(IEnumerable<CustomerDto> Customers);
