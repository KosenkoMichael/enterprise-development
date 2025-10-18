namespace CarRentalService.Dtos.Dto;

/// <summary>
/// Collection for top N customers
/// </summary>
/// <param name="CustomerTotalSpentDto">customer and totalspent</param>
public sealed record CustomerTotalSpentCollectionResponse(IEnumerable<CustomerTotalSpentDto> CustomerTotalSpentDto);
