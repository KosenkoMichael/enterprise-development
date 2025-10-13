using CarRentalService.Application.Dto;
using CarRentalService.Application.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for vehicle model management operations.
/// </summary>
/// <param name="service">Vehicle model service instance</param>
[Route("api/[controller]")]
[ApiController]
public class VehicleModelsController(VehicleModelService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all vehicle models.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<VehicleModel>> GetAll() =>
        await service.GetVehicleModels();

    /// <summary>
    /// Retrieves a vehicle model by ID.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<VehicleModel?> GetById(Guid id) =>
        await service.GetVehicleModel(id);

    /// <summary>
    /// Creates a new vehicle model.
    /// </summary>
    /// <param name="vehicleModelDto">Vehicle model data</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<Guid> Create([FromBody] VehicleModelDto vehicleModelDto) =>
        await service.CreateVehicleModel(vehicleModelDto);

    /// <summary>
    /// Updates an existing vehicle model.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    /// <param name="vehicleModelDto">Updated vehicle model data</param>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<VehicleModel?> Update(Guid id, [FromBody] VehicleModelDto vehicleModelDto) =>
        await service.UpdateVehicleModel(id, vehicleModelDto);

    /// <summary>
    /// Deletes a vehicle model by ID.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteVehicleModel(id);
}