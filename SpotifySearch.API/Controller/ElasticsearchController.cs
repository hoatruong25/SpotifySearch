using Microsoft.AspNetCore.Mvc;
using SpotifySearchAPI.BusinessService.ElasticsearchService;
using SpotifySearchAPI.BusinessService.SpotifyIngestService;
using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class ElasticsearchController : ControllerBase
{
    private readonly IElasticSearchService _elasticSearchService;
    private readonly ISpotifyService _spotifyService;

    public ElasticsearchController(IElasticSearchService elasticSearchService, ISpotifyService spotifyService)
    {
        _elasticSearchService = elasticSearchService;
        _spotifyService = spotifyService;
    }

    [HttpPost("insert/{index}")]
    public async Task<IActionResult> InsertDocumentAsync(string index, SpotifyPlay spotifyPlay, CancellationToken cancellation)
    {
        var result = await _elasticSearchService.InsertDocumentAsync(spotifyPlay, index, cancellation);
        return result ? Ok() : StatusCode(500, "Failed to insert document");
    }

    [HttpPost("bulk/{index}")]
    public async Task<IActionResult> BulkDocumentForDatasetAsync(string index, CancellationToken cancellation)
    {
        var result = await _spotifyService.BulkAsync(index, cancellation);
        return result ? Ok() : StatusCode(500, "Failed to bulk insert documents");
    }
}