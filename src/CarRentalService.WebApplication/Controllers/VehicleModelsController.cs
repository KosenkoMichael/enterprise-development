using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for managing vehicle models in the car rental system.
/// </summary>
/// <param name="service">Service handling vehicle model operations.</param>
[Route("api/[controller]")]
[ApiController]
public class VehicleModelsController(VehicleModelService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all vehicle models.
    /// </summary>
    /// <returns>A list of all vehicle models.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<VehicleModel>> GetAll() =>
        await service.GetVehicleModels();

    /// <summary>
    /// Retrieves a specific vehicle model by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model.</param>
    /// <returns>The vehicle model if found; otherwise, null.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<VehicleModel?> GetById(Guid id) =>
        await service.GetVehicleModel(id);

    /// <summary>
    /// Creates a new vehicle model.
    /// </summary>
    /// <param name="vehicleModelDto">The DTO containing vehicle model information.</param>
    /// <returns>The unique identifier of the newly created vehicle model.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<Guid> Create([FromBody] VehicleModelDto vehicleModelDto) =>
        await service.CreateVehicleModel(vehicleModelDto);

    /// <summary>
    /// Updates an existing vehicle model.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to update.</param>
    /// <param name="vehicleModelDto">The DTO containing updated vehicle model information.</param>
    /// <returns>The updated vehicle model if successful; otherwise, null.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<VehicleModel?> Update(Guid id, [FromBody] VehicleModelDto vehicleModelDto) =>
        await service.UpdateVehicleModel(id, vehicleModelDto);

    /// <summary>
    /// Deletes a vehicle model by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteVehicleModel(id);
}
