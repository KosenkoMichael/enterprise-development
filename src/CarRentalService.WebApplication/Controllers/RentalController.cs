using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Mappers;
using CarRentalService.WebApplication.Services;
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<RentalCollectionResponse> GetAll() =>
        (await service.GetRentalsAsync()).ToResponse();

    /// <summary>
    /// Retrieves a specific rental by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the rental.</param>
    /// <returns>The rental if found; otherwise, null.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDto>> GetById(Guid id)
    {
        var result = await service.GetRentalAsync(id);
        if (result is null) return NotFound();
        return result.ToDto();
    }
    /// <summary>
    /// Creates a new rental.
    /// </summary>
    /// <param name="rentalDto">The DTO containing rental information.</param>
    /// <returns>The unique identifier of the newly created rental.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] RentalRequest rentalDto)
    {
        var result = await service.CreateRentalAsync(rentalDto.ToDomain());
        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    /// <summary>
    /// Updates an existing rental.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to update.</param>
    /// <param name="rentalDto">The DTO containing updated rental information.</param>
    /// <returns>The updated rental if successful; otherwise, null.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDto>> Update(Guid id, [FromBody] RentalRequest rentalDto)
    {
        var result = await service.UpdateRentalAsync(id, rentalDto.ToDomain());
        if (result is null) return NotFound();
        return result.ToDto();
    }
    /// <summary>
    /// Deletes a rental by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the rental to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteRentalAsync(id);
}
