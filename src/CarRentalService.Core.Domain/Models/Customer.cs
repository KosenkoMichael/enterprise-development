namespace CarRentalService.Core.Domain.Models;
public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string DriverLicenseNumber { get; set; }
    public required string FullName { get; set; }
    public required DateTime DateOfBirth { get; set; }
}