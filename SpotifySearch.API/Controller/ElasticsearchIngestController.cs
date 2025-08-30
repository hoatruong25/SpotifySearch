using Microsoft.AspNetCore.Mvc;
using SpotifySearchAPI.BusinessService.ElasticsearchService;
using SpotifySearchAPI.BusinessService.SpotifyIngestService;
using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class ElasticsearchIngestController : ControllerBase
{
    private readonly IElasticsearchService _elasticsearchService;
    private readonly ISpotifyService _spotifyService;

    public ElasticsearchIngestController(IElasticsearchService elasticsearchService, ISpotifyService spotifyService)
    {
        _elasticsearchService = elasticsearchService;
        _spotifyService = spotifyService;
    }

    [HttpPost("insert/{index}")]
    public async Task<IActionResult> InsertDocumentAsync(string index, SpotifyPlay spotifyPlay, CancellationToken cancellation)
    {
        var result = await _elasticsearchService.InsertDocumentAsync(spotifyPlay, index, cancellation);
        return result ? Ok() : StatusCode(500, "Failed to insert document");
    }

    [HttpPost("bulk/{index}")]
    public async Task<IActionResult> BulkDocumentForDatasetAsync(string index, CancellationToken cancellation)
    {
        var result = await _spotifyService.BulkAsync(index, cancellation);
        return result ? Ok() : StatusCode(500, "Failed to bulk insert documents");
    }

    [HttpGet("search/{index}")]
    public async Task<IActionResult> SearchFullTextAsync(string index, string fieldName, string query, int size,
        CancellationToken cancellation)
    {
        // var result = await _elasticSearchService.SearchFullTextAsync<SpotifyPlay>(index, fieldName, query, size, cancellation);
        var result = await _spotifyService.SearchFullTextAsync(index, fieldName, query, size, cancellation);
        return result.Any() ? Ok(result) : StatusCode(500, "No results found");
    }
}