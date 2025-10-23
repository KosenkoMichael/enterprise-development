namespace CarRentalService.WebApplication.Dto;

/// <summary>
///  Represents a customer in the car rental system.
/// </summary>
/// <param name="Id"> Unique identifier for the customer. </param>
/// <param name="DriverLicenseNumber"> Driver's license number of the customer. </param>
/// <param name="FullName"> Full name of the customer. </param>
/// <param name="DateOfBirth"> Date of birth of the customer. </param>
public sealed record CustomerDto(Guid Id, string DriverLicenseNumber, string FullName, DateTime DateOfBirth);
