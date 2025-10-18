using CarRentalService.Core.Domain.Service;
using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Mappers;
using CarRentalService.WebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for managing customers in the car rental system.
/// </summary>
/// <param name="service">Service handling customer operations.</param>
/// /// <param name="logger">logger.</param>
[Route("api/[controller]")]
[ApiController]
public class CustomersController(ICustomerService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    /// <returns>A list of all customers.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<CustomerCollectionResponse> GetAll()
    {
        logger.LogInformation("called GetAll in CustomersController");
        return (await service.GetCustomersAsync()).ToResponse();
    }

    /// <summary>
    /// Retrieves a specific customer by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <returns>The customer if found; otherwise, null.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> GetById(Guid id)
    {
        logger.LogInformation("called GetById in CustomersController");
        var result = await service.GetCustomerAsync(id);
        if (result is null) return NotFound();
        return result.ToDto();
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerDto">The DTO containing customer information.</param>
    /// <returns>The unique identifier of the newly created customer.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] CustomerRequest customerDto)
    {
        logger.LogInformation("called Create in CustomersController");
        var result = await service.CreateCustomerAsync(customerDto.ToDomain());
        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }
    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to update.</param>
    /// <param name="customerDto">The DTO containing updated customer information.</param>
    /// <returns>The updated customer if successful; otherwise, null.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> Update(Guid id, [FromBody] CustomerRequest customerDto)
    {
        logger.LogInformation("called Update in CustomersController");
        var result = await service.UpdateCustomerAsync(id, customerDto.ToDomain());
        if (result is null) return NotFound();
        return result.ToDto();
    }

    /// <summary>
    /// Deletes a customer by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<bool> Delete(Guid id)
    {
        logger.LogInformation("called Delete in CustomersController");
        return await service.DeleteCustomerAsync(id);
    }
}
