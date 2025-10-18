namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Represents a customer in the car rental system.
/// </summary>
public class CustomerRequest
{

    /// <summary>
    /// The unique driver's license number of the customer.
    /// </summary>
    public required string DriverLicenseNumber { get; set; }

    /// <summary>
    /// The full name of the customer.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// The date of birth of the customer.
    /// </summary>
    public required DateTime DateOfBirth { get; set; }
}