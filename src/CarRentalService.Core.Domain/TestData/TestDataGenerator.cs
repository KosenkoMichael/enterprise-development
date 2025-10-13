using CarRentalService.Core.Domain.Models;
using System.Drawing;

namespace CarRentalService.Core.Domain.TestData;

/// <summary>
/// Provides methods for generating test data used in unit tests.
/// </summary>
public class TestDataGenerator
{
    /// <summary>
    /// Generates comprehensive test data for fixture initialization.
    /// </summary>
    /// <returns>
    /// A tuple containing five lists:
    /// <list type="bullet">
    /// <item><description><see cref="List{VehicleModel}"/> - Vehicle models with basic specifications</description></item>
    /// <item><description><see cref="List{ModelGeneration}"/> - Model generations with technical details and pricing</description></item>
    /// <item><description><see cref="List{Vehicle}"/> - Individual vehicles with color and license plate information</description></item>
    /// <item><description><see cref="List{Customer}"/> - Customer profiles with driver license and personal data</description></item>
    /// <item><description><see cref="List{Rental}"/> - Rental records with vehicle assignments and time periods</description></item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// The generated data includes 10 records for each entity type with realistic relationships
    /// between models, generations, vehicles, customers, and rentals.
    /// </remarks>
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
            new() { Model = vehicleModels[0], Year = 2018, EngineVolume = 1.6, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 40m },
            new() { Model = vehicleModels[0], Year = 2019, EngineVolume = 2.0, TransmissionType = TransmissionType.Manual, RentalPricePerHour = 50m },
            new() { Model = vehicleModels[1], Year = 2018, EngineVolume = 2.0, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 45m },
            new() { Model = vehicleModels[1], Year = 2019, EngineVolume = 2.4, TransmissionType = TransmissionType.Manual, RentalPricePerHour = 55m },
            new() { Model = vehicleModels[2], Year = 2018, EngineVolume = 1.6, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 35m },
            new() { Model = vehicleModels[2], Year = 2019, EngineVolume = 2.0, TransmissionType = TransmissionType.Manual, RentalPricePerHour = 45m },
            new() { Model = vehicleModels[3], Year = 2019, EngineVolume = 1.5, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 38m },
            new() { Model = vehicleModels[4], Year = 2018, EngineVolume = 2.5, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 60m },
            new() { Model = vehicleModels[5], Year = 2020, EngineVolume = 2.0, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 52m },
            new() { Model = vehicleModels[6], Year = 2021, EngineVolume = 3.0, TransmissionType = TransmissionType.Automatic, RentalPricePerHour = 70m }
        };

        // Vehicles
        var vehicles = new List<Vehicle>
        {
            new() { Generation = modelGenerations[0], Color = Color.Black, LicensePlate = "A001BCRUS" },
            new() { Generation = modelGenerations[1], Color = Color.White, LicensePlate = "A002BCRUS" },
            new() { Generation = modelGenerations[2], Color = Color.Silver, LicensePlate = "A003BCRUS" },
            new() { Generation = modelGenerations[3], Color = Color.Red, LicensePlate = "A004BCRUS" },
            new() { Generation = modelGenerations[4], Color = Color.Blue, LicensePlate = "A005BCRUS" },
            new() { Generation = modelGenerations[5], Color = Color.Black, LicensePlate = "A006BCRUS" },
            new() { Generation = modelGenerations[6], Color = Color.White, LicensePlate = "A007BCRUS" },
            new() { Generation = modelGenerations[7], Color = Color.Gray, LicensePlate = "A008BCRUS" },
            new() { Generation = modelGenerations[8], Color = Color.Yellow, LicensePlate = "A009BCRUS" },
            new() { Generation = modelGenerations[9], Color = Color.Green, LicensePlate = "A010BCRUS" }
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
            new() { RentedVehicle = vehicles[0], Customer = customers[0], RentStartTime = now.AddHours(-2), RentalDurationHours = 8 },
            new() { RentedVehicle = vehicles[1], Customer = customers[1], RentStartTime = now.AddHours(-3), RentalDurationHours = 6 },
            new() { RentedVehicle = vehicles[2], Customer = customers[2], RentStartTime = now.AddDays(-1), RentalDurationHours = 10 },
            new() { RentedVehicle = vehicles[3], Customer = customers[3], RentStartTime = now.AddDays(-2), RentalDurationHours = 12 },
            new() { RentedVehicle = vehicles[4], Customer = customers[4], RentStartTime = now.AddDays(-3), RentalDurationHours = 14 },
            new() { RentedVehicle = vehicles[5], Customer = customers[5], RentStartTime = now.AddDays(-4), RentalDurationHours = 9 },
            new() { RentedVehicle = vehicles[6], Customer = customers[6], RentStartTime = now.AddDays(-5), RentalDurationHours = 11 },
            new() { RentedVehicle = vehicles[7], Customer = customers[7], RentStartTime = now.AddDays(-6), RentalDurationHours = 7 },
            new() { RentedVehicle = vehicles[8], Customer = customers[8], RentStartTime = now.AddDays(-7), RentalDurationHours = 13 },
            new() { RentedVehicle = vehicles[9], Customer = customers[9], RentStartTime = now.AddDays(-8), RentalDurationHours = 15 }
        };

        return (vehicleModels, modelGenerations, vehicles, customers, rentals);
    }
}