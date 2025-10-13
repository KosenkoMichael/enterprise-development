using CarRentalService.Application.Dto;
using CarRentalService.Application.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for model generation management operations.
/// </summary>
/// <param name="service">Model generation service instance</param>
[Route("api/[controller]")]
[ApiController]
public class ModelGenerationsController(ModelGenerationService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all model generations.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<ModelGeneration>> GetAll() =>
        await service.GetModelGenerationsAsync();

    /// <summary>
    /// Retrieves a model generation by ID.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ModelGeneration?> GetByIdAsync(Guid id) =>
        await service.GetModelGenerationAsync(id);

    /// <summary>
    /// Creates a new model generation.
    /// </summary>
    /// <param name="modelGenerationDto">Model generation data</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<Guid> Create([FromBody] ModelGenerationDto modelGenerationDto) =>
        await service.CreateModelGenerationAsync(modelGenerationDto);

    /// <summary>
    /// Updates an existing model generation.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    /// <param name="modelGenerationDto">Updated model generation data</param>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ModelGeneration?> Update(Guid id, [FromBody] ModelGenerationDto modelGenerationDto) =>
        await service.UpdateModelGenerationAsync(id, modelGenerationDto);

    /// <summary>
    /// Deletes a model generation by ID.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteModelGenerationAsync(id);
}