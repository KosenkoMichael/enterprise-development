using CarRentalService.Application.Dto;
using CarRentalService.Application.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RentalsController(RentalService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<Rental>> GetAll() =>
        await service.GetRentalsAsync();

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Rental?> GetByIdAsync(Guid id) =>
        await service.GetRentalAsync(id);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<Guid> Create([FromBody] RentalDto rentalDto) =>
        await service.CreateRentalAsync(rentalDto);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Rental?> Update(Guid id, [FromBody] RentalDto rentalDto) =>
        await service.UpdateRentalAsync(id, rentalDto);

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteRentalAsync(id);
}
