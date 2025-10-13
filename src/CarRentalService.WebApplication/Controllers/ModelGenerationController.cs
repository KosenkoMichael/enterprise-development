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
public class ModelGenerationController(ModelGenerationService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all model generations.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<ModelGeneration> GetAll() =>
        service.GetModelGenerations();

    /// <summary>
    /// Retrieves a model generation by ID.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ModelGeneration? GetById(Guid id) =>
        service.GetModelGeneration(id);

    /// <summary>
    /// Creates a new model generation.
    /// </summary>
    /// <param name="modelGenerationDto">Model generation data</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Guid Create([FromBody] ModelGenerationDto modelGenerationDto) =>
        service.CreateModelGeneration(modelGenerationDto);

    /// <summary>
    /// Updates an existing model generation.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    /// <param name="modelGenerationDto">Updated model generation data</param>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ModelGeneration? Update(Guid id, [FromBody] ModelGenerationDto modelGenerationDto) =>
        service.UpdateModelGeneration(id, modelGenerationDto);

    /// <summary>
    /// Deletes a model generation by ID.
    /// </summary>
    /// <param name="id">Model generation identifier</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public bool Delete(Guid id)
    {
        var existing = service.GetModelGeneration(id);
        if (existing == null)
        {
            return false;
        }
        return service.DeleteModelGeneration(id);
    }
}