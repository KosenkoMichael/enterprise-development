using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Mappers;
using CarRentalService.WebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for managing vehicles in the car rental system.
/// </summary>
/// <param name="service">Service handling vehicle operations.</param>
[Route("api/[controller]")]
[ApiController]
public class VehiclesController(VehicleService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all vehicles.
    /// </summary>
    /// <returns>A list of all vehicles.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<VehicleCollectionResponse> GetAll() =>
        (await service.GetVehiclesAsync()).ToResponse();

    /// <summary>
    /// Retrieves a specific vehicle by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle.</param>
    /// <returns>The vehicle if found; otherwise, null.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleDto>> GetById(Guid id) {
        var result = await service.GetVehicleAsync(id);
        if (result is null) return NotFound();
        return result.ToDto();
    }
    /// <summary>
    /// Creates a new vehicle.
    /// </summary>
    /// <param name="vehicleDto">The DTO containing vehicle information.</param>
    /// <returns>The unique identifier of the newly created vehicle.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] VehicleRequest vehicleDto)
    {
        var result = await service.CreateVehicleAsync(vehicleDto);
        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    /// <summary>
    /// Updates an existing vehicle.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to update.</param>
    /// <param name="vehicleDto">The DTO containing updated vehicle information.</param>
    /// <returns>The updated vehicle if successful; otherwise, null.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleDto>> Update(Guid id, [FromBody] VehicleRequest vehicleDto) {
        var result = await service.UpdateVehicleAsync(id, vehicleDto);
        if (result is null) return NotFound();
        return result.ToDto();
    }
    /// <summary>
    /// Deletes a vehicle by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteVehicleAsync(id);
}
