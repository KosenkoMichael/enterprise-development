using CarRentalService.WebApplication.Services;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Tests.Fixtures;
using Xunit;

namespace CarRentalService.Core.Tests;

/// <summary>
/// Unit tests for AnalyticsService using generated test data.
/// </summary>
/// <remarks>
/// Constructor that initializes the AnalyticsServiceTests with the provided fixture.
/// </remarks>
/// <param name="fixture">fixture for tests </param>
public class AnalyticsServiceTests(AnalyticsServiceFixture fixture) : IClassFixture<AnalyticsServiceFixture>
{
    private readonly AnalyticsService _service = fixture.Service;

    /// <summary>
    /// GetCustomersByVehicleModelAsync returns customers ordered by full name
    /// </summary>
    [Fact(DisplayName = "GetCustomersByVehicleModelAsync returns customers ordered by full name")]
    public async void GetCustomersByVehicleModelAsync_Returns_Ordered_Customers()
    {
        var modelId = fixture.VehicleModels[0].Id;
        var result = await _service.GetCustomersByVehicleModelAsync(modelId);

        var expectedNames = new[] { "Johnson Michael", "Smith John" };
        Assert.Equal(expectedNames, result.Select(x => x.FullName));
    }

    /// <summary>
    /// GetVehiclesCurrentlyRentedAsync returns correct vehicle details
    /// </summary>
    [Fact(DisplayName = "GetVehiclesCurrentlyRentedAsync returns correct vehicle details")]
    public async void GetVehiclesCurrentlyRentedAsync_Returns_RentedVehicles()
    {
        var result = await _service.GetVehiclesCurrentlyRentedAsync();

        var expectedLicensePlate = "A001BCRUS";
        var expectedColor = "Black";

        var expectedVehicle = result.First();

        Assert.Equal(expectedLicensePlate, expectedVehicle.LicensePlate);
        Assert.Equal(expectedColor, expectedVehicle.Color);
    }

    /// <summary>
    /// GetTopRentedVehiclesAsync returns top 5 vehicles
    /// </summary>
    [Fact(DisplayName = "GetTopRentedVehiclesAsync returns top 5 vehicles")]
    public async void GetTopRentedVehiclesAsync_Returns_Top5()
    {
        var result = await _service.GetTopRentedVehiclesAsync();
        var expected = fixture.Vehicles.Take(5).Select(v => v.Id).ToList();

        Assert.Equal(expected, [.. result.Select(x => x.Vehicle.Id)]);
    }

    /// <summary>
    /// GetRentalCountPerVehicleAsync returns counts for all vehicles
    /// </summary>
    [Fact(DisplayName = "GetRentalCountPerVehicleAsync returns counts for all vehicles")]
    public async void GetRentalCountPerVehicleAsync_Returns_AllVehicles()
    {
        var result = await _service.GetRentalCountPerVehicleAsync();

        var expected = fixture.Vehicles.Select(v => (VehicleId: v.Id, Count: 1)).ToList();
        Assert.Equal(expected, result.Select(x => (x.Vehicle.Id, x.RentalCount)).ToList());
    }

    /// <summary>
    /// GetTopCustomersByRentalSumAsync returns top 5 customers
    /// </summary>
    [Fact(DisplayName = "GetTopCustomersByRentalSumAsync returns top 5 customers")]
    public async void GetTopCustomersByRentalSumAsync_Returns_Top5Customers()
    {
        var result = await _service.GetTopCustomersByRentalSumAsync();

        var expected = new[]
        {
            ("Wilson Mia", 1050m),
            ("Lopez Noah", 676m),
            ("Brown Sophia", 660m),
            ("Jones David", 490m),
            ("Williams Emma", 450m)
        };

        Assert.Equal(expected, result.Select(x => (x.Customer.FullName, x.TotalSpent)));
    }
}
