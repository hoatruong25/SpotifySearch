using Microsoft.AspNetCore.Mvc;
using SpotifySearchAPI.BusinessService.UberService;

namespace SpotifySearchAPI.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class UberController : ControllerBase
{
    private readonly IUberService _uberService;
    public UberController(IUberService uberService)
    {
        _uberService = uberService;
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> BulkRideBooking(CancellationToken cancellation)
    {
        var result = await _uberService.BulkAsync(cancellation);
        return result ? Ok() : StatusCode(500, "Failed to bulk insert documents");
    }
}