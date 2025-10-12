namespace CarRentalService.Application.Dto;
/// <summary>
/// Customer Data Transfer Object
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// Driver's license number of the customer.
    /// </summary>
    public required string DriverLicenseNumber { get; set; }
    /// <summary>
    /// Full name of the customer.
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// Date of birth of the customer.
    /// </summary>
    public required DateTime DateOfBirth { get; set; }
}
