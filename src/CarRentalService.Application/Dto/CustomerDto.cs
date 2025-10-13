namespace CarRentalService.Application.Dto;

/// <summary>
/// Represents customer information for data transfer between layers.
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// Gets or sets the customer's driver license number.
    /// </summary>
    /// <value>A string representing the official driver license identifier.</value>
    public required string DriverLicenseNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the customer's full name.
    /// </summary>
    /// <value>A string containing the complete name of the customer.</value>
    public required string FullName { get; set; }
    
    /// <summary>
    /// Gets or sets the customer's date of birth.
    /// </summary>
    /// <value>A <see cref="DateTime"/> value representing the birth date.</value>
    public required DateTime DateOfBirth { get; set; }
}