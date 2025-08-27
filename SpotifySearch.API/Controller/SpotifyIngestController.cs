using Microsoft.AspNetCore.Mvc;
using SpotifySearchAPI.BusinessService.SpotifyIngestService;

namespace SpotifySearchAPI.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class SpotifyIngestController : ControllerBase
{
    private readonly ISpotifyIngestService _spotifyIngestService;

    public SpotifyIngestController(ISpotifyIngestService spotifyIngestService)
    {
        _spotifyIngestService = spotifyIngestService;
    }

    [HttpPost]
    public async Task<IActionResult> BulkAsync(CancellationToken cancellation)
    {
        var result = await _spotifyIngestService.BulkAsync(cancellation);
        
        if (result)
            return Ok("Success");
        return BadRequest(result);
    }
}