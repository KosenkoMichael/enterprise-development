using CarRentalService.Producer.Web.Fakers;
using Microsoft.AspNetCore.Mvc;
using NATS.Client.JetStream;
using System.Text.Json;

namespace CarRentalService.Producer.Web.Controllers;

/// <summary>
/// Сontroller for sending customers via NATS
/// </summary>
/// <param name="context">jet stream context</param>
[ApiController]
[Route("api/[controller]")]
public class CustomersController(INatsJSContext context) : ControllerBase
{
    /// <summary>
    /// Send customers via NATS
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Generate([FromForm] int count)
    {
        var data = CustomerFaker.Generate(count);

        await context.PublishAsync("car-rental.customers.create",
            JsonSerializer.SerializeToUtf8Bytes(data));

        return Accepted();
    }
}
