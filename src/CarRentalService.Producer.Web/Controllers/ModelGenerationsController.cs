using CarRentalService.Core.Contracts.Dto;
using CarRentalService.Producer.Web.Fakers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NATS.Client.JetStream;
using System.Text.Json;

namespace CarRentalService.Producer.Web.Controllers;

/// <summary>
/// Сontroller for sending model generations via NATS
/// </summary>
/// <param name="context">jet stream context</param>
/// <param name="factory">http client factory</param>
/// <param name="json">json options</param>
[ApiController]
[Route("api/[controller]")]
public class ModelGenerationsController(
    INatsJSContext context,
    IHttpClientFactory factory,
    IOptions<JsonSerializerOptions> json) : ControllerBase
{
    private readonly JsonSerializerOptions _json = json.Value;

    /// <summary>
    /// Send model generations via NATS
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Generate([FromForm] int count)
    {
        var http = factory.CreateClient("CarRentalApi");

        var models = await http.GetFromJsonAsync<VehicleModelCollectionResponse>(
            "/api/vehiclemodels", _json);

        if (models is null)
            return Problem("Vehicle model list is empty.");

        var ids = models.VehicleModels.Select(m => m.Id).ToList();

        var data = ModelGenerationFaker.Generate(count, ids);

        await context.PublishAsync("car-rental.modelgenerations.create",
            JsonSerializer.SerializeToUtf8Bytes(data));

        return Accepted();
    }
}
