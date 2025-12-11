using CarRentalService.Core.Contracts.Dto;
using CarRentalService.Producer.Web.Fakers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NATS.Client.JetStream;
using System.Text.Json;

namespace CarRentalService.Producer.Web.Controllers;

/// <summary>
/// Сontroller for sending vehicles via NATS
/// </summary>
/// <param name="context">jet stream context</param>
/// <param name="factory">http client factory</param>
/// <param name="json">json options</param>
[ApiController]
[Route("api/[controller]")]
public class VehiclesController(INatsJSContext context,
    IHttpClientFactory factory,
    IOptions<JsonSerializerOptions> json) : ControllerBase
{
    private readonly JsonSerializerOptions _json = json.Value;

    /// <summary>
    /// Send vehicles via NATS
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Generate([FromForm] int count)
    {
        var http = factory.CreateClient("CarRentalApi");

        var gens = await http.GetFromJsonAsync<ModelGenerationCollectionResponse>(
            "/api/modelgenerations", _json);

        if (gens is null)
            return Problem("Modelgeneration list is empty.");

        var ids = gens.ModelGenerations.Select(g => g.Id).ToList();

        var data = VehicleFaker.Generate(count, ids);

        await context.PublishAsync("car-rental.vehicles.create",
            JsonSerializer.SerializeToUtf8Bytes(data));

        return Accepted();
    }
}
