using CarRentalService.Core.Domain.Service;
using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Mappers;
using CarRentalService.WebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for managing vehicle models in the car rental system.
/// </summary>
/// <param name="service">Service handling vehicle model operations.</param>
/// /// <param name="logger">Service handling vehicle model operations.</param>
[Route("api/[controller]")]
[ApiController]
public class VehicleModelsController(IVehicleModelService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves all vehicle models.
    /// </summary>
    /// <returns>A list of all vehicle models.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<VehicleModelCollectionResponse> GetAll()
    {
        logger.LogInformation("called GetAll in VehicleModelsController");
        return (await service.GetVehicleModels()).ToResponse();
    }

    /// <summary>
    /// Retrieves a specific vehicle model by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model.</param>
    /// <returns>The vehicle model if found; otherwise, null.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleModelDto>> GetById(Guid id)
    {
        logger.LogInformation("called GetById in VehicleModelsController");
        var result = await service.GetVehicleModel(id);
        if (result is null) return NotFound();
        return result.ToDto();
    }
    /// <summary>
    /// Creates a new vehicle model.
    /// </summary>
    /// <param name="vehicleModelDto">The DTO containing vehicle model information.</param>
    /// <returns>The unique identifier of the newly created vehicle model.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] VehicleModelRequest vehicleModelDto)
    {
        logger.LogInformation("called Create in VehicleModelsController");
        var result = await service.CreateVehicleModel(vehicleModelDto.ToDomain());
        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    /// <summary>
    /// Updates an existing vehicle model.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to update.</param>
    /// <param name="vehicleModelDto">The DTO containing updated vehicle model information.</param>
    /// <returns>The updated vehicle model if successful; otherwise, null.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleModelDto>> Update(Guid id, [FromBody] VehicleModelRequest vehicleModelDto)
    {
        logger.LogInformation("called Update in VehicleModelsController");
        var result = await service.UpdateVehicleModel(id, vehicleModelDto.ToDomain());
        if (result is null) return NotFound();
        return result.ToDto();
    }
    /// <summary>
    /// Deletes a vehicle model by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<bool> Delete(Guid id)
    {
        logger.LogInformation("called Delete in VehicleModelsController");
        return await service.DeleteVehicleModel(id);
    }
}
