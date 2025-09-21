using CarRentalService.Core.Tests.Fixtures;

namespace CarRentalService.Core.Tests;
/// <summary>
/// Unit tests for CarRentalServise's modules
/// </summary>
public class RentalServiceTests : IClassFixture<TestDataFixture>
{
    private readonly TestDataFixture _fixture;
    /// <summary>
    /// Initializes constructor for tests
    /// </summary>
    /// <param name="fixture"></param>
    public RentalServiceTests(TestDataFixture fixture)
    {
        _fixture = fixture;
    }
    /// <summary>
    /// Displays information about all customers who rented cars of the specified model, sorted by full name.
    /// </summary>
    [Fact]
    public void GetCustomersRentingModel_OrderedByName()
    {
        var targetModel = "Camry";

        var result = _fixture.Rentals
            .Where(r => r.RentedVehicle.Generation.Model.Name == targetModel)
            .Select(r => r.Customer)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        var expected = (from r in _fixture.Rentals
                        where r.RentedVehicle.Generation.Model.Name == targetModel
                        orderby r.Customer.FullName
                        select r.Customer
                        )
                      .Distinct()
                      .ToList();
        Assert.NotEmpty(expected);
        Assert.NotEmpty(result);
        Assert.Equal(expected, result);
    }
    /// <summary>
    /// Displays information about currently rented cars.
    /// </summary>
    [Fact]
    public void GetVehiclesCurrentlyRented()
    {
        var now = DateTime.Now;

        var result = _fixture.Rentals
            .Where(r => r.RentStartTime <= now &&
                        r.RentStartTime.AddHours(r.RentalDurationHours) > now)
            .Select(r => r.RentedVehicle)
            .Distinct()
            .ToList();

        var expected = (from r in _fixture.Rentals
                        where r.RentStartTime <= now &&
                              r.RentStartTime.AddHours(r.RentalDurationHours) > now
                        select r.RentedVehicle)
                      .Distinct()
                      .ToList();
        Assert.NotEmpty(expected);
        Assert.NotEmpty(result);
        Assert.Equal(expected, result);
    }
    /// <summary>
    /// Displays the top 5 most frequently rented cars.
    /// </summary>
    [Fact]
    public void GetTop5MostFrequentlyRentedVehicles()
    {
        var result = _fixture.Rentals
            .GroupBy(r => r.RentedVehicle)
            .Select(g => new { Vehicle = g.Key, RentalCount = g.Count() })
            .OrderByDescending(x => x.RentalCount)
            .Take(5)
            .ToList();

        var expected = (from r in _fixture.Rentals
                        group r by r.RentedVehicle into g
                        orderby g.Count() descending
                        select new { Vehicle = g.Key, RentalCount = g.Count() })
                      .Take(5)
                      .ToList();
        Assert.NotEmpty(expected);
        Assert.NotEmpty(result);
        Assert.Equal(expected, result);
    }
    /// <summary>
    /// For each car, displays the rental amount.
    /// </summary>
    [Fact]
    public void GetRentalCountPerVehicle()
    {
        var result = _fixture.Vehicles
            .Select(vehicle => new
            {
                Vehicle = vehicle,
                RentalCount = _fixture.Rentals.Count(r => r.RentedVehicle.Id == vehicle.Id)
            })
            .ToList();

        var expected = (from vehicle in _fixture.Vehicles
                        select new
                        {
                            Vehicle = vehicle,
                            RentalCount = (from r in _fixture.Rentals
                                           where r.RentedVehicle.Id == vehicle.Id
                                           select r).Count()
                        })
                      .ToList();
        Assert.NotEmpty(expected);
        Assert.NotEmpty(result);
        Assert.Equal(expected, result);
    }
    /// <summary>
    /// Displays the top 5 customers by rental amount.
    /// </summary>
    [Fact]
    public void GetTop5CustomersByRentalCost()
    {
        var result = _fixture.Rentals
            .GroupBy(r => r.Customer)
            .Select(g => new
            {
                Customer = g.Key,
                TotalCost = g.Sum(r => r.TotalCost)
            })
            .OrderByDescending(x => x.TotalCost)
            .Take(5)
            .ToList();

        var expected = (from r in _fixture.Rentals
                        group r by r.Customer into g
                        orderby g.Sum(r => r.TotalCost) descending
                        select new
                        {
                            Customer = g.Key,
                            TotalCost = g.Sum(r => r.TotalCost)
                        })
                      .Take(5)
                      .ToList();
        Assert.NotEmpty(expected);
        Assert.NotEmpty(result);
        Assert.Equal(expected, result);
    }
}