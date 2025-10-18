namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Dto for top N customers
/// </summary>
/// <param name="Customer">Customer</param>
/// <param name="TotalSpent">Customer</param>
public record CustomerTotalSpentDto(CustomerDto Customer, decimal TotalSpent);