using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for managing model generations in the car rental system.
/// </summary>
/// <param name="service">Service handling model generation operations.</param>
[Route("api/[controller]")]
[ApiController]
public class ModelGenerationsController(ModelGenerationService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all model generations.
    /// </summary>
    /// <returns>A list of all model generations.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<ModelGeneration>> GetAll() =>
        await service.GetModelGenerationsAsync();

    /// <summary>
    /// Retrieves a specific model generation by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation.</param>
    /// <returns>The model generation if found; otherwise, null.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ModelGeneration?> GetByIdAsync(Guid id) =>
        await service.GetModelGenerationAsync(id);

    /// <summary>
    /// Creates a new model generation.
    /// </summary>
    /// <param name="modelGenerationDto">The DTO containing model generation information.</param>
    /// <returns>The unique identifier of the newly created model generation.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<Guid> Create([FromBody] ModelGenerationDto modelGenerationDto) =>
        await service.CreateModelGenerationAsync(modelGenerationDto);

    /// <summary>
    /// Updates an existing model generation.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to update.</param>
    /// <param name="modelGenerationDto">The DTO containing updated model generation information.</param>
    /// <returns>The updated model generation if successful; otherwise, null.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ModelGeneration?> Update(Guid id, [FromBody] ModelGenerationDto modelGenerationDto) =>
        await service.UpdateModelGenerationAsync(id, modelGenerationDto);

    /// <summary>
    /// Deletes a model generation by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteModelGenerationAsync(id);
}
