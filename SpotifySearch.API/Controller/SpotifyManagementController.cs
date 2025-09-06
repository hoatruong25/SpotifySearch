using Microsoft.AspNetCore.Mvc;
using SpotifySearchAPI.BusinessService.SpotifyIngestService;

namespace SpotifySearchAPI.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class SpotifyManagementController : ControllerBase
{
    private readonly ISpotifyService _spotifyService;

    public SpotifyManagementController(ISpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }
}