using CarRentalService.Core.Contracts.Dto;
using CarRentalService.Core.Contracts.Mappers;
using CarRentalService.Core.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for managing vehicles in the car rental system.
/// </summary>
/// <param name="service">Service handling vehicle operations.</param>
/// /// <param name="logger">logger.</param>
[Route("api/[controller]")]
[ApiController]
public class VehiclesController(IVehicleService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves all vehicles.
    /// </summary>
    /// <returns>A list of all vehicles.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<VehicleCollectionResponse> GetAll()
    {
        logger.LogInformation("called GetAll in VehiclesController");
        return (await service.GetVehiclesAsync()).ToResponse();
    }
    /// <summary>
    /// Retrieves a specific vehicle by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle.</param>
    /// <returns>The vehicle if found; otherwise, null.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleDto>> GetById(Guid id)
    {
        logger.LogInformation("called GetById in VehiclesController");
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
        logger.LogInformation("called Create in VehiclesController");
        var result = await service.CreateVehicleAsync(vehicleDto.ToDomain());
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
    public async Task<ActionResult<VehicleDto>> Update(Guid id, [FromBody] VehicleRequest vehicleDto)
    {
        logger.LogInformation("called Update in VehiclesController");
        var result = await service.UpdateVehicleAsync(id, vehicleDto.ToDomain());
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
    public async Task<bool> Delete(Guid id)
    {
        logger.LogInformation("called Delete in VehiclesController");
        return await service.DeleteVehicleAsync(id);
    }

    /// <summary>
    /// Returns model generation, related with this vehicle
    /// </summary>
    /// <param name="id">id of vehicle</param>
    /// <returns>model generation</returns>
    [HttpGet("{id:guid}/modelgeneration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ModelGenerationDto>> GetRelatedModelGeneration(Guid id)
    {
        logger.LogInformation("called GetRelatedModelGeneration in VehiclesController");
        var result = await service.GetRelatedModelGeneration(id);
        if (result is null) return NotFound();
        return result.ToDto();
    }
}
