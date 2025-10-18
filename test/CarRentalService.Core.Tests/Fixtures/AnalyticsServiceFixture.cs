using CarRentalService.WebApplication.Services;
using CarRentalService.Core.Domain.Models;
using CarRentalService.Core.Domain.Repository;
using CarRentalService.Core.Domain.DataSeed;
using Moq;
using CarRentalService.WebApplication.Dto;

namespace CarRentalService.Core.Tests.Fixtures;

/// <summary>
/// Provides a fully configured instance of AnalyticsService with mocked repositories and generated test data.
/// </summary>
public class AnalyticsServiceFixture
{
    
    /// <summary>
    /// Service instance with mocked dependencies.
    /// </summary>
    public AnalyticsService Service { get; }

    /// <summary>
    /// Vehicle models used in tests.
    /// </summary>
    public List<VehicleModel> VehicleModels { get; }

    /// <summary>
    /// Model generations used in tests.
    /// </summary>
    public List<ModelGeneration> ModelGenerations { get; }

    /// <summary>
    /// Vehicles used in tests.
    /// </summary>
    public List<Vehicle> Vehicles { get; }


    /// <summary>
    /// Customers used in tests.
    /// </summary>
    public List<Customer> Customers { get; }

    /// <summary>
    /// Rentals used in tests.
    /// </summary>
    public List<Rental> Rentals { get; }

    /// <summary>
    /// Constructor initializes the AnalyticsService with mocked repositories and generated test data.
    /// </summary>
    public AnalyticsServiceFixture()
    {
        var generator = new DataSeeder();
        (VehicleModels, ModelGenerations, Vehicles, Customers, Rentals) = generator.GenerateTestData();

        var vehicleModelRepoMock = new Mock<IRepository<VehicleModel>>();
        var modelGenRepoMock = new Mock<IRepository<ModelGeneration>>();
        var vehicleRepoMock = new Mock<IRepository<Vehicle>>();
        var customerRepoMock = new Mock<IRepository<Customer>>();
        var rentalRepoMock = new Mock<IRepository<Rental>>();

        vehicleModelRepoMock.Setup(x => x.ReadAllAsync()).ReturnsAsync(VehicleModels);
        modelGenRepoMock.Setup(x => x.ReadAllAsync()).ReturnsAsync(ModelGenerations);
        vehicleRepoMock.Setup(x => x.ReadAllAsync()).ReturnsAsync(Vehicles);
        customerRepoMock.Setup(x => x.ReadAllAsync()).ReturnsAsync(Customers);
        rentalRepoMock.Setup(x => x.ReadAllAsync()).ReturnsAsync(Rentals);

        Service = new AnalyticsService(
            rentalRepoMock.Object,
            vehicleRepoMock.Object,
            customerRepoMock.Object,
            modelGenRepoMock.Object
        );
    }
}
