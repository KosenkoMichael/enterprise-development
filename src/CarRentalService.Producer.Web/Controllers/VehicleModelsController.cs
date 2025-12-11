using CarRentalService.Producer.Web.Fakers;
using Microsoft.AspNetCore.Mvc;
using NATS.Client.JetStream;
using System.Text.Json;

namespace CarRentalService.Producer.Web.Controllers;

/// <summary>
/// Сontroller for sending vehicle models via NATS
/// </summary>
/// <param name="context">jet stream context</param>
[ApiController]
[Route("api/[controller]")]
public class VehicleModelsController(INatsJSContext context) : ControllerBase
{
    /// <summary>
    /// Send vehicle models via NATS
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Generate([FromForm] int count)
    {
        var data = VehicleModelFaker.Generate(count);

        await context.PublishAsync("car-rental.vehiclemodels.create",
            JsonSerializer.SerializeToUtf8Bytes(data));

        return Accepted();
    }
}
