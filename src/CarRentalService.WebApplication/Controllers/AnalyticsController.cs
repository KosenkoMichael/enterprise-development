using CarRentalService.Application.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller providing analytics endpoints for rentals, vehicles, and customers.
/// </summary>
/// <param name="service">Analytics service handling complex queries.</param>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(AnalyticsService service) : ControllerBase
{
    /// <summary>
    /// Returns all customers who rented vehicles of a specified model, ordered by full name.
    /// </summary>
    [HttpGet("customers-by-model/{vehicleModelId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<Customer>>> GetCustomersByVehicleModel(Guid vehicleModelId)
    {
        var result = await service.GetCustomersByVehicleModelAsync(vehicleModelId);
        if (result.Count == 0) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Returns all vehicles that are currently rented.
    /// </summary>
    [HttpGet("vehicles-rented")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<Vehicle>>> GetVehiclesCurrentlyRented()
    {
        var result = await service.GetVehiclesCurrentlyRentedAsync();
        if (result.Count == 0) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Returns top 5 most frequently rented vehicles.
    /// </summary>
    [HttpGet("top-vehicles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<object>>> GetTopVehicles()
    {
        var result = (await service.GetTopRentedVehiclesAsync())
            .Select(x => new { x.Vehicle, x.RentalCount })
            .ToList<object>();

        return Ok(result);
    }

    /// <summary>
    /// Returns the number of rentals for each vehicle.
    /// </summary>
    [HttpGet("rental-count-per-vehicle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<object>>> GetRentalCountPerVehicle()
    {
        var result = (await service.GetRentalCountPerVehicleAsync())
            .Select(x => new { x.Vehicle, x.RentalCount })
            .ToList<object>();

        return Ok(result);
    }

    /// <summary>
    /// Returns top 5 customers by total amount spent on rentals.
    /// </summary>
    [HttpGet("top-customers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<object>>> GetTopCustomersByRentalSum()
    {
        var result = (await service.GetTopCustomersByRentalSumAsync())
            .Select(x => new { x.Customer, x.TotalSpent })
            .ToList<object>();

        return Ok(result);
    }
}
