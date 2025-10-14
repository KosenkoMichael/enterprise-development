using CarRentalService.WebApplication.Dto;
using CarRentalService.WebApplication.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for managing customers in the car rental system.
/// </summary>
/// <param name="service">Service handling customer operations.</param>
[Route("api/[controller]")]
[ApiController]
public class CustomersController(CustomerService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    /// <returns>A list of all customers.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<Customer>> GetAll() =>
        await service.GetCustomersAsync();

    /// <summary>
    /// Retrieves a specific customer by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <returns>The customer if found; otherwise, null.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Customer?> GetById(Guid id) =>
        await service.GetCustomerAsync(id);

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerDto">The DTO containing customer information.</param>
    /// <returns>The unique identifier of the newly created customer.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<Guid> Create([FromBody] CustomerDto customerDto) =>
        await service.CreateCustomerAsync(customerDto);

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to update.</param>
    /// <param name="customerDto">The DTO containing updated customer information.</param>
    /// <returns>The updated customer if successful; otherwise, null.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Customer?> Update(Guid id, [FromBody] CustomerDto customerDto) =>
        await service.UpdateCustomerAsync(id, customerDto);

    /// <summary>
    /// Deletes a customer by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the customer to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteCustomerAsync(id);
}
