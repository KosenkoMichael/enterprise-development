using CarRentalService.Core.Domain.Models;
using System.Drawing;

namespace CarRentalService.Core.Domain.DataSeed;

/// <summary>
/// Test data generator for the car rental system.
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Generates test data for the car rental system, including vehicle models, model generations, vehicles, customers, and rentals.
    /// </summary>
    /// <returns></returns>
    public (List<VehicleModel>, List<ModelGeneration>, List<Vehicle>, List<Customer>, List<Rental>) GenerateTestData()
    {
        var now = DateTime.Now;

        // Vehicle Models
        var vehicleModels = new List<VehicleModel>
        {
            new() { Name = "Camry", DriveType = Models.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Sedan, Class = VehicleClass.Business },
            new() { Name = "RAV4", DriveType = Models.DriveType.AWD, SeatCount = 5, BodyType = BodyType.SUV, Class = VehicleClass.Compact },
            new() { Name = "Corolla", DriveType = Models.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Sedan, Class = VehicleClass.Economy },
            new() { Name = "Civic", DriveType = Models.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Sedan, Class = VehicleClass.Economy },
            new() { Name = "CR-V", DriveType = Models.DriveType.AWD, SeatCount = 5, BodyType = BodyType.SUV, Class = VehicleClass.Compact },
            new() { Name = "Accord", DriveType = Models.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Sedan, Class = VehicleClass.Business },
            new() { Name = "Highlander", DriveType = Models.DriveType.AWD, SeatCount = 7, BodyType = BodyType.SUV, Class = VehicleClass.Luxury },
            new() { Name = "Mazda3", DriveType = Models.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Hatchback, Class = VehicleClass.Compact },
            new() { Name = "Altima", DriveType = Models.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Sedan, Class = VehicleClass.Business },
            new() { Name = "CX-5", DriveType = Models.DriveType.AWD, SeatCount = 5, BodyType = BodyType.SUV, Class = VehicleClass.Compact }
        };

        // Model Generations
        var modelGenerations = new List<ModelGeneration>
        {
            new() { VehicleModelId = vehicleModels[0].Id, Year = 2018, EngineVolume = 1.6, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 40m },
            new() { VehicleModelId = vehicleModels[0].Id, Year = 2019, EngineVolume = 2.0, TransmissionType = TransmissionType.Manual, RentalPricePerHour = 50m },
            new() { VehicleModelId = vehicleModels[1].Id, Year = 2018, EngineVolume = 2.0, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 45m },
            new() { VehicleModelId = vehicleModels[1].Id, Year = 2019, EngineVolume = 2.4, TransmissionType = TransmissionType.Manual, RentalPricePerHour = 55m },
            new() { VehicleModelId = vehicleModels[2].Id, Year = 2018, EngineVolume = 1.6, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 35m },
            new() { VehicleModelId = vehicleModels[2].Id, Year = 2019, EngineVolume = 2.0, TransmissionType = TransmissionType.Manual, RentalPricePerHour = 45m },
            new() { VehicleModelId = vehicleModels[3].Id, Year = 2019, EngineVolume = 1.5, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 38m },
            new() { VehicleModelId = vehicleModels[4].Id, Year = 2018, EngineVolume = 2.5, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 60m },
            new() { VehicleModelId = vehicleModels[5].Id, Year = 2020, EngineVolume = 2.0, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 52m },
            new() { VehicleModelId = vehicleModels[6].Id, Year = 2021, EngineVolume = 3.0, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 70m }
        };

        // Vehicles
        var vehicles = new List<Vehicle>
        {
            new() { ModelGenerationId = modelGenerations[0].Id, Color = "Black", LicensePlate = "A001BCRUS" },
            new() { ModelGenerationId = modelGenerations[1].Id, Color = "White", LicensePlate = "A002BCRUS" },
            new() { ModelGenerationId = modelGenerations[2].Id, Color = "Silver", LicensePlate = "A003BCRUS" },
            new() { ModelGenerationId = modelGenerations[3].Id, Color = "Red", LicensePlate = "A004BCRUS" },
            new() { ModelGenerationId = modelGenerations[4].Id, Color = "Blue", LicensePlate = "A005BCRUS" },
            new() { ModelGenerationId = modelGenerations[5].Id, Color = "Black", LicensePlate = "A006BCRUS" },
            new() { ModelGenerationId = modelGenerations[6].Id, Color = "White", LicensePlate = "A007BCRUS" },
            new() { ModelGenerationId = modelGenerations[7].Id, Color = "Gray", LicensePlate = "A008BCRUS" },
            new() { ModelGenerationId = modelGenerations[8].Id, Color = "Yellow", LicensePlate = "A009BCRUS" },
            new() { ModelGenerationId = modelGenerations[9].Id, Color = "Green", LicensePlate = "A010BCRUS" }
        };

        // Customers
        var customers = new List<Customer>
        {
            new() { DriverLicenseNumber = "77AB100001", FullName = "Smith John", DateOfBirth = new DateTime(1985,1,1) },
            new() { DriverLicenseNumber = "77AB100002", FullName = "Johnson Michael", DateOfBirth = new DateTime(1986,2,2) },
            new() { DriverLicenseNumber = "77AB100003", FullName = "Williams Emma", DateOfBirth = new DateTime(1987,3,3) },
            new() { DriverLicenseNumber = "77AB100004", FullName = "Brown Sophia", DateOfBirth = new DateTime(1988,4,4) },
            new() { DriverLicenseNumber = "77AB100005", FullName = "Jones David", DateOfBirth = new DateTime(1989,5,5) },
            new() { DriverLicenseNumber = "77AB100006", FullName = "Garcia Olivia", DateOfBirth = new DateTime(1990,6,6) },
            new() { DriverLicenseNumber = "77AB100007", FullName = "Martinez Liam", DateOfBirth = new DateTime(1991,7,7) },
            new() { DriverLicenseNumber = "77AB100008", FullName = "Davis Ava", DateOfBirth = new DateTime(1992,8,8) },
            new() { DriverLicenseNumber = "77AB100009", FullName = "Lopez Noah", DateOfBirth = new DateTime(1993,9,9) },
            new() { DriverLicenseNumber = "77AB100010", FullName = "Wilson Mia", DateOfBirth = new DateTime(1994,10,10) }
        };

        // Rentals
        var rentals = new List<Rental>
        {
            new() { VehicleId = vehicles[0].Id, CustomerId = customers[0].Id, RentStartTime = now.AddHours(-2), RentalDurationHours = 8 },
            new() { VehicleId = vehicles[1].Id, CustomerId = customers[1].Id, RentStartTime = now.AddHours(-3), RentalDurationHours = 6 },
            new() { VehicleId = vehicles[2].Id, CustomerId = customers[2].Id, RentStartTime = now.AddDays(-1), RentalDurationHours = 10 },
            new() { VehicleId = vehicles[3].Id, CustomerId = customers[3].Id, RentStartTime = now.AddDays(-2), RentalDurationHours = 12 },
            new() { VehicleId = vehicles[4].Id, CustomerId = customers[4].Id, RentStartTime = now.AddDays(-3), RentalDurationHours = 14 },
            new() { VehicleId = vehicles[5].Id, CustomerId = customers[5].Id, RentStartTime = now.AddDays(-4), RentalDurationHours = 9 },
            new() { VehicleId = vehicles[6].Id, CustomerId = customers[6].Id, RentStartTime = now.AddDays(-5), RentalDurationHours = 11 },
            new() { VehicleId = vehicles[7].Id, CustomerId = customers[7].Id, RentStartTime = now.AddDays(-6), RentalDurationHours = 7 },
            new() { VehicleId = vehicles[8].Id, CustomerId = customers[8].Id, RentStartTime = now.AddDays(-7), RentalDurationHours = 13 },
            new() { VehicleId = vehicles[9].Id, CustomerId = customers[9].Id, RentStartTime = now.AddDays(-8), RentalDurationHours = 15 }
        };

        return (vehicleModels, modelGenerations, vehicles, customers, rentals);
    }
}