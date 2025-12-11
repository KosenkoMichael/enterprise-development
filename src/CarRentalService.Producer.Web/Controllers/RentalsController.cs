using CarRentalService.Core.Contracts.Dto;
using CarRentalService.Producer.Web.Fakers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NATS.Client.JetStream;
using System.Text.Json;

namespace CarRentalService.Producer.Web.Controllers;

/// <summary>
/// Сontroller for sending rentals via NATS
/// </summary>
/// <param name="context">jet stream context</param>
/// <param name="factory">http client factory</param>
/// <param name="json">json options</param>
[ApiController]
[Route("api/[controller]")]
public class RentalsController(
    INatsJSContext context,
    IHttpClientFactory factory,
    IOptions<JsonSerializerOptions> json) : ControllerBase
{
    private readonly JsonSerializerOptions _json = json.Value;

    /// <summary>
    /// Send rentals via NATS
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Generate([FromForm] int count)
    {
        var http = factory.CreateClient("CarRentalApi");

        var customers = await http.GetFromJsonAsync<CustomerCollectionResponse>(
            "/api/customers", _json);

        var vehicles = await http.GetFromJsonAsync<VehicleCollectionResponse>(
            "/api/vehicles", _json);

        if (customers is null)
            return Problem("Customer list is empty.");

        if (vehicles is null)
            return Problem("Vehicle list is empty.");

        var custIds = customers.Customers.Select(c => c.Id).ToList();
        var vehIds = vehicles.Vehicles.Select(v => v.Id).ToList();

        var data = RentalFaker.Generate(count, vehIds, custIds);

        await context.PublishAsync("car-rental.rentals.create",
            JsonSerializer.SerializeToUtf8Bytes(data));

        return Accepted();
    }
}
