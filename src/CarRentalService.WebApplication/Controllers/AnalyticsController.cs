using CarRentalService.Core.Domain.Service;
using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller providing analytics endpoints for rentals, vehicles, and customers.
/// </summary>
/// <param name="service">Analytics service handling complex queries.</param>
/// /// <param name="logger">logger.</param>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Returns all customers who rented vehicles of a specified model, ordered by full name.
    /// </summary>
    [HttpGet("customers-by-model/{vehicleModelId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerCollectionResponse>> GetCustomersByVehicleModel(Guid vehicleModelId)
    {
        logger.LogInformation("called GetCustomersByVehicleModel");
        var result = await service.GetCustomersByVehicleModelAsync(vehicleModelId);
        if (result.Count == 0) return NotFound();
        return result.ToResponse();
    }

    /// <summary>
    /// Returns all vehicles that are currently rented.
    /// </summary>
    [HttpGet("vehicles-rented")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleCollectionResponse>> GetVehiclesCurrentlyRented()
    {
        logger.LogInformation("called GetVehiclesCurrentlyRented");
        var result = await service.GetVehiclesCurrentlyRentedAsync();
        if (result.Count == 0) return NotFound();
        return result.ToResponse();
    }

    /// <summary>
    /// Returns top 5 most frequently rented vehicles.
    /// </summary>
    [HttpGet("top-vehicles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<VehicleRentalCountCollectionResponse>> GetTopVehicles()
    {
        logger.LogInformation("called GetTopVehicles");
        var result = (await service.GetTopRentedVehiclesAsync())
            .Select(x => new VehicleRentalCountDto(x.Vehicle.ToDto(), x.RentalCount))
            .ToList();

        return result.ToResponse();
    }

    /// <summary>
    /// Returns the number of rentals for each vehicle.
    /// </summary>
    [HttpGet("rental-count-per-vehicle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<VehicleRentalCountCollectionResponse>> GetRentalCountPerVehicle()
    {
        logger.LogInformation("called GetRentalCountPerVehicle");
        var result = (await service.GetRentalCountPerVehicleAsync())
            .Select(x => new VehicleRentalCountDto(x.Vehicle.ToDto(), x.RentalCount))
            .ToList();

        return result.ToResponse();
    }

    /// <summary>
    /// Returns top 5 customers by total amount spent on rentals.
    /// </summary>
    [HttpGet("top-customers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CustomerTotalSpentCollectionResponse>> GetTopCustomersByRentalSum()
    {
        logger.LogInformation("called GetTopCustomersByRentalSum");
        var result = (await service.GetTopCustomersByRentalSumAsync())
            .Select(x => new CustomerTotalSpentDto(x.Customer.ToDto(), x.TotalSpent))
            .ToList();

        return result.ToResponse();
    }
}
