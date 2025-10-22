using CarRentalService.Core.Domain.Models;
using CarRentalService.WebApplication.Dto;

namespace CarRentalService.WebApplication.Mappers;
internal static class CarRentalMapper
{
    public static CustomerDto ToDto(this Customer customer) => new(customer.Id, customer.DriverLicenseNumber,
                                                                    customer.FullName, customer.DateOfBirth);
    public static ModelGenerationDto ToDto(this ModelGeneration modelGeneration) => new(
        modelGeneration.Id, modelGeneration.Year, modelGeneration.EngineVolume, modelGeneration.TransmissionType,
        modelGeneration.VehicleModelId, modelGeneration.RentalPricePerHour);
    public static RentalDto ToDto(this Rental rental) => new(rental.Id, rental.VehicleId, rental.CustomerId,
                                                             rental.RentStartTime, rental.RentalDurationHours,
                                                             rental.TotalCost);
    public static VehicleDto ToDto(this Vehicle vehicle) => new(vehicle.Id, vehicle.ModelGenerationId,
                                                                vehicle.LicensePlate, vehicle.Color);
    public static VehicleModelDto ToDto(this VehicleModel vehicleModel) => new(vehicleModel.Id, vehicleModel.Name,
                                                                               vehicleModel.DriveType,
                                                                               vehicleModel.SeatCount,
                                                                               vehicleModel.BodyType, vehicleModel.Class);
    public static Customer ToDomain(this CustomerRequest customer) => new()
    {
        DriverLicenseNumber = customer.DriverLicenseNumber,
        DateOfBirth = customer.DateOfBirth,
        FullName = customer.FullName,
    };
    public static ModelGeneration ToDomain(this ModelGenerationRequest modelGeneration) => new()
    {
        Year = modelGeneration.Year,
        EngineVolume = modelGeneration.EngineVolume,
        TransmissionType = modelGeneration.TransmissionType,
        VehicleModelId = modelGeneration.VehicleModelId,
        RentalPricePerHour = modelGeneration.RentalPricePerHour,
    };
    public static Rental ToDomain(this RentalRequest rental) => new()
    {
        VehicleId = rental.VehicleId,
        CustomerId = rental.CustomerId,
        RentalDurationHours = rental.RentalDurationHours,
        RentStartTime = rental.RentStartTime,
    };
    public static Vehicle ToDomain(this VehicleRequest vehicle) => new()
    {
        ModelGenerationId = vehicle.ModelGenerationId,
        LicensePlate = vehicle.LicensePlate,
        Color = vehicle.Color,
    };
    public static VehicleModel ToDomain(this VehicleModelRequest vehicleModel) => new()
    {
        Name = vehicleModel.Name,
        DriveType = vehicleModel.DriveType,
        SeatCount = vehicleModel.SeatCount,
        BodyType = vehicleModel.BodyType,
        Class = vehicleModel.Class,
    };
    public static CustomerCollectionResponse ToResponse(this List<Customer> customers) => new(customers.Select(ToDto));
    public static ModelGenerationCollectionResponse ToResponse(this List<ModelGeneration> modelGenerations) => new(modelGenerations.Select(ToDto));
    public static RentalCollectionResponse ToResponse(this List<Rental> rentals) => new(rentals.Select(ToDto));
    public static VehicleCollectionResponse ToResponse(this List<Vehicle> vehicles) => new(vehicles.Select(ToDto));
    public static VehicleModelCollectionResponse ToResponse(this List<VehicleModel> vehicleModels) => new(vehicleModels.Select(ToDto));
    public static VehicleRentalCountCollectionResponse ToResponse(this List<VehicleRentalCountDto> vehicleRentalCountDto) => new(vehicleRentalCountDto);
    public static CustomerTotalSpentCollectionResponse ToResponse(this List<CustomerTotalSpentDto> customerTotalSpentDto) => new(customerTotalSpentDto);
}
