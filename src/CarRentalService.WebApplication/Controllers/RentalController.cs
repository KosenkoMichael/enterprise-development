using CarRentalService.Application.Dto;
using CarRentalService.Application.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for managing rentals in the car rental system.
/// </summary>
/// <param name="service">Service handling rental operations.</param>
[Route("api/[controller]")]
[ApiController]
public class RentalsController(RentalService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all rentals.
    /// </summary>
    /// <returns>A list of all rentals.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<Rental>> GetAll() =>
        await service.GetRentalsAsync();

    /// <summary>
    /// Retrieves a specific rental by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the rental.</param>
    /// <returns>The rental if found; otherwise, null.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Rental?> GetByIdAsync(Guid id) =>
        await service.GetRentalAsync(id);

    /// <summary>
    /// Creates a new rental.
    /// </summary>
    /// <param name="rentalDto">The DTO containing rental information.</param>
    /// <returns>The unique identifier of the newly created rental.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<Guid> Create([FromBody] RentalDto rentalDto) =>
        await service.CreateRentalAsync(rentalDto);

    /// <summary>
    /// Updates an existing rental.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to update.</param>
    /// <param name="rentalDto">The DTO containing updated rental information.</param>
    /// <returns>The updated rental if successful; otherwise, null.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Rental?> Update(Guid id, [FromBody] RentalDto rentalDto) =>
        await service.UpdateRentalAsync(id, rentalDto);

    /// <summary>
    /// Deletes a rental by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteRentalAsync(id);
}
