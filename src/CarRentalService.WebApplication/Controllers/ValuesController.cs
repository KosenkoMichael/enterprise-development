using CarRentalService.Application.Dto;
using CarRentalService.Application.Services;
using CarRentalService.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.WebApplication.Controllers;
/// <summary>
/// API Controller for managing customers
/// </summary>
/// <param name="service"></param>

[Route("api/[controller]")]
[ApiController]
public class CustomerController(CustomerService service): ControllerBase
{
    /// <summary>
    /// Gets all customers
    /// </summary>
    /// <returns>List of customers</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<Customer> GetAll()
    {
        return service.GetCustomers();
    }
    /// <summary>
    /// Gets a customer by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Single customer</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Customer? GetById(Guid id)
    {
        return service.GetCustomer(id);
    }
    /// <summary>
    /// Creates a new customer
    /// </summary>
    /// <param name="customerDto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Guid Create([FromBody] CustomerDto customerDto)
    {
        return service.CreateStudent(customerDto);
    }
    /// <summary>
    /// Updates an existing customer
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customerDto"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Customer? Update(Guid id, [FromBody] CustomerDto customerDto)
    {
        return service.UpdateCustomer(id, customerDto);
    }
    /// <summary>
    /// Deletes a customer by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public bool Delete(Guid id)
    {
        return service.DeleteCustomer(id);
    }
}
