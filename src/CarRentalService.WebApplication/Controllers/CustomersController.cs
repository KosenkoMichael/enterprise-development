using CarRentalService.Application.Dto;
using CarRentalService.Application.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;

/// <summary>
/// Controller for customer management operations.
/// </summary>
/// <param name="service">Customer service instance</param>
[Route("api/[controller]")]
[ApiController]
public class CustomersController(CustomerService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<Customer>> GetAll() =>
        await service.GetCustomersAsync();

    /// <summary>
    /// Retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="id">Customer identifier</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Customer?> GetById(Guid id) =>
        await service.GetCustomerAsync(id);

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerDto">Customer data</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<Guid> Create([FromBody] CustomerDto customerDto) =>
        await service.CreateCustomerAsync(customerDto);

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">Customer identifier</param>
    /// <param name="customerDto">Updated customer data</param>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<Customer?> Update(Guid id, [FromBody] CustomerDto customerDto) =>
        await service.UpdateCustomerAsync(id, customerDto);

    /// <summary>
    /// Deletes a customer by their identifier.
    /// </summary>
    /// <param name="id">Customer identifier</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<bool> Delete(Guid id) =>
        await service.DeleteCustomerAsync(id);
}