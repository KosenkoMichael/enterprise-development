using CarRentalService.Core.Domain.Service;
using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Mappers;
using CarRentalService.WebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for managing model generations in the car rental system.
/// </summary>
/// <param name="service">Service handling model generation operations.</param>
[Route("api/[controller]")]
[ApiController]
public class ModelGenerationsController(IModelGenerationService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all model generations.
    /// </summary>
    /// <returns>A list of all model generations.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ModelGenerationCollectionResponse> GetAll() =>
        (await service.GetModelGenerationsAsync()).ToResponse();

    /// <summary>
    /// Retrieves a specific model generation by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation.</param>
    /// <returns>The model generation if found; otherwise, null.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ModelGenerationDto>> GetById(Guid id)
    {
        var result = await service.GetModelGenerationAsync(id);
        if (result is null) return NotFound();
        return result.ToDto();
    }
    /// <summary>
    /// Creates a new model generation.
    /// </summary>
    /// <param name="modelGenerationDto">The DTO containing model generation information.</param>
    /// <returns>The unique identifier of the newly created model generation.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] ModelGenerationRequest modelGenerationDto)
    {
        var result = await service.CreateModelGenerationAsync(modelGenerationDto.ToDomain());
        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    /// <summary>
    /// Updates an existing model generation.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to update.</param>
    /// <param name="modelGenerationDto">The DTO containing updated model generation information.</param>
    /// <returns>The updated model generation if successful; otherwise, null.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ModelGenerationDto>> Update(Guid id, [FromBody] ModelGenerationRequest modelGenerationDto)
    {
        var result = await service.UpdateModelGenerationAsync(id, modelGenerationDto.ToDomain());
        if (result is null) return NotFound();
        return result.ToDto();
    }
    /// <summary>
    /// Deletes a model generation by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteModelGenerationAsync(id);

    /// <summary>
    /// Returns vehicle model, related with this model generation
    /// </summary>
    /// <param name="id">id of model generation</param>
    /// <returns>vehicle model</returns>
    [HttpGet("{id:guid}/vehiclemodel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleModelDto>> GetRelatedVehicleModel(Guid id)
    {
        var result = await service.GetRelatedVehicleModel(id);
        if (result is null) return NotFound();
        return result.ToDto();
    }
}
