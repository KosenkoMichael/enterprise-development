namespace CarRentalService.Core.Domain.Models;
/// <summary>
///  Represents a customer in the car rental system.
/// </summary>
public class Customer
{
    /// <summary>
    /// Unique identifier for the customer.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
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

    /// <summary>
    /// Navigation property to rentals
    /// </summary>
    public ICollection<Rental> Rentals { get; set; } = null!;
}