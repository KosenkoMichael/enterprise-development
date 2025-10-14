using CarRentalService.Application.Dto;
using CarRentalService.Application.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VehiclesController(VehicleService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<Vehicle>> GetAll() =>
        await service.GetVehiclesAsync();

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Vehicle?> GetByIdAsync(Guid id) =>
        await service.GetVehicleAsync(id);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<Guid> Create([FromBody] VehicleDto vehicleDto) =>
        await service.CreateVehicleAsync(vehicleDto);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Vehicle?> Update(Guid id, [FromBody] VehicleDto vehicleDto) =>
        await service.UpdateVehicleAsync(id, vehicleDto);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteVehicleAsync(id);
}
