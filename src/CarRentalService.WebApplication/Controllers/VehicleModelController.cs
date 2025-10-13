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
public class VehicleModelController(VehicleModelService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all vehicle models.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<VehicleModel> GetAll() =>
        service.GetVehicleModels();

    /// <summary>
    /// Retrieves a vehicle model by ID.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public VehicleModel? GetById(Guid id) =>
        service.GetVehicleModel(id);

    /// <summary>
    /// Creates a new vehicle model.
    /// </summary>
    /// <param name="vehicleModelDto">Vehicle model data</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Guid Create([FromBody] VehicleModelDto vehicleModelDto) =>
        service.CreateVehicleModel(vehicleModelDto);

    /// <summary>
    /// Updates an existing vehicle model.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    /// <param name="vehicleModelDto">Updated vehicle model data</param>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public VehicleModel? Update(Guid id, [FromBody] VehicleModelDto vehicleModelDto) =>
        service.UpdateVehicleModel(id, vehicleModelDto);

    /// <summary>
    /// Deletes a vehicle model by ID.
    /// </summary>
    /// <param name="id">Vehicle model identifier</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public bool Delete(Guid id) =>
        service.DeleteVehicleModel(id);
}