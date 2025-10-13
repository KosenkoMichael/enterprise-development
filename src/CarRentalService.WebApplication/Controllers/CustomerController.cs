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
public class CustomerController(CustomerService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<Customer> GetAll() =>
        service.GetCustomers();

    /// <summary>
    /// Retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="id">Customer identifier</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Customer? GetById(Guid id) =>
        service.GetCustomer(id);

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerDto">Customer data</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Guid Create([FromBody] CustomerDto customerDto) =>
        service.CreateCustomer(customerDto);

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">Customer identifier</param>
    /// <param name="customerDto">Updated customer data</param>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Customer? Update(Guid id, [FromBody] CustomerDto customerDto) =>
        service.UpdateCustomer(id, customerDto);

    /// <summary>
    /// Deletes a customer by their identifier.
    /// </summary>
    /// <param name="id">Customer identifier</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public bool Delete(Guid id) =>
        service.DeleteCustomer(id);
}