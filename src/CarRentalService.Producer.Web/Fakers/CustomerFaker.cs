using Bogus;
using CarRentalService.Core.Contracts.Dto;

namespace CarRentalService.Producer.Web.Fakers;

/// <summary>
/// Faker for customer
/// </summary>
public static class CustomerFaker
{
    private static readonly Faker<CustomerRequest> _faker = new Faker<CustomerRequest>()
        .CustomInstantiator(f => new CustomerRequest(
            DriverLicenseNumber: $"{f.Random.Number(1, 99):00}{f.Random.String2(2)}{f.Random.Number(100000, 999999):000000}",
            FullName: $"{f.Name.LastName()} {f.Name.FirstName()}",
            DateOfBirth: f.Date.Between(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-18))
        ));

    /// <summary>
    /// Generate N random CustomerRequests
    /// </summary>
    /// <param name="count">count</param>
    /// <returns>List of randomly generated CustomerRequests </returns>
    public static List<CustomerRequest> Generate(int count)
        => _faker.Generate(count);
}
