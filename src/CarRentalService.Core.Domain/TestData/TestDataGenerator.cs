using CarRentalService.Core.Domain.Models;
using System.Drawing;

namespace CarRentalService.Core.Domain.TestData;
/// <summary>
/// Test data generator class for Unit test
/// </summary>
public class TestDataGenerator
{
    /// <summary>
    /// Generates Test Data for fixture
    /// </summary>
    /// <returns></returns>
    public (List<VehicleModel>, List<ModelGeneration>, List<Vehicle>, List<Customer>, List<Rental>) GenerateTestData()
    {
        var now = DateTime.Now;

        var vehicleModels = new List<VehicleModel>
        {
            new VehicleModel() { Name = "Camry", DriveType = Models.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Sedan, Class = VehicleClass.Business },
            new VehicleModel() { Name = "RAV4", DriveType = Models.DriveType.AWD, SeatCount = 5, BodyType = BodyType.SUV, Class = VehicleClass.Compact },
            new VehicleModel() { Name = "Corolla", DriveType = Models.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Sedan, Class = VehicleClass.Economy },
            new VehicleModel() { Name = "Civic", DriveType = Models.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Sedan, Class = VehicleClass.Economy },
            new VehicleModel() { Name = "CR-V", DriveType = Models.DriveType.AWD, SeatCount = 5, BodyType = BodyType.SUV, Class = VehicleClass.Compact }
        };

        var modelGenerations = new List<ModelGeneration>();
        foreach (var model in vehicleModels)
        {
            for (var i = 0; i < 3; i++)
            {
                modelGenerations.Add(new ModelGeneration
                {
                    Year = 2018 + i,
                    EngineVolume = 1.6 + i * 0.4,
                    TransmissionType = i % 2 == 0 ? TransmissionType.Automatic : TransmissionType.Manual,
                    Model = model,
                    RentalPricePerHour = 40m + i * 10
                });
            }
        }

        var vehicles = new List<Vehicle>();
        var colors = new[] { Color.Black , Color.White, Color.Silver, Color.Red, Color.Blue };
        var colorIndex = 0;
        var plateNumber = 100;

        foreach (var generation in modelGenerations)
        {
            for (var i = 0; i < 2; i++)
            {
                vehicles.Add(new Vehicle
                {
                    LicensePlate = $"A{plateNumber:000}BCRUS",
                    Color = colors[colorIndex % colors.Length],
                    Generation = generation
                });
                colorIndex++;
            }
        }

        var customers = new List<Customer>();
        var firstNames = new[] { "John", "Michael", "Emma", "Sophia", "David", "Olivia" };
        var lastNames = new[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia" };

        for (var i = 0; i < 10; i++)
        {
            customers.Add(new Customer
            {
                DriverLicenseNumber = $"77AB{100000 + i}",
                FullName = $"{lastNames[i % lastNames.Length]} {firstNames[i % firstNames.Length]}",
                DateOfBirth = new DateTime(1985, 1, 1).AddDays(i * 365)
            });
        }

        var rentals = new List<Rental>();
        var currentlyRentedVehicles = vehicles.Take(3).ToList();

        for (var i = 0; i < currentlyRentedVehicles.Count; i++)
        {
            rentals.Add(new Rental
            {
                RentedVehicle = currentlyRentedVehicles[i],
                Customer = customers[i % customers.Count],
                RentStartTime = now.AddHours(-2),
                RentalDurationHours = 8
            });
        }

        var popularVehicle = vehicles[5];
        for (var i = 0; i < 5; i++)
        {
            rentals.Add(new Rental
            {
                RentedVehicle = popularVehicle,
                Customer = customers[(i + 1) % customers.Count],
                RentStartTime = now.AddDays(-i * 10),
                RentalDurationHours = 12 + i * 6
            });
        }

        for (var i = 0; i < 15; i++)
        {
            var vehicleIndex = (i + 7) % vehicles.Count;
            var vehicle = vehicles[vehicleIndex];

            if (currentlyRentedVehicles.Contains(vehicle)) continue;

            rentals.Add(new Rental
            {
                RentedVehicle = vehicle,
                Customer = customers[(i + 2) % customers.Count],
                RentStartTime = now.AddDays(-(i + 1) * 7),
                RentalDurationHours = 24 + i * 3
            });
        }

        return (vehicleModels, modelGenerations, vehicles, customers, rentals);
    }
}