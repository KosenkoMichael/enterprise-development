namespace CarRentalService.WebApplication.Dto;

/// <summary>
/// Customer request
/// </summary>
/// <param name="DriverLicenseNumber">Driver license number</param>
/// <param name="FullName">Full name</param>
/// <param name="DateOfBirth">Date of birth</param>
public sealed record CustomerRequest(string DriverLicenseNumber, string FullName, DateTime DateOfBirth);