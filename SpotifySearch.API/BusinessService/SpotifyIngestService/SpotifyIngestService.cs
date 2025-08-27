using SpotifySearchAPI.BusinessService.ElasticsearchService;
using SpotifySearchAPI.BusinessService.NormalizerService;
using SpotifySearchAPI.Repository;
using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.BusinessService.SpotifyIngestService;

public class SpotifyIngestService : ISpotifyIngestService
{
    private readonly ISpotifyTrackRepository _spotifyTrackRepository;
    private readonly INormalizerService _normalizerService;
    private readonly IElasticSearchService _elasticSearchService;
    private const int BatchSize = 50; // Số lượng documents mỗi batch

    public SpotifyIngestService(ISpotifyTrackRepository spotifyTrackRepository, INormalizerService normalizerService, IElasticSearchService elasticSearchService)
    {
        _spotifyTrackRepository = spotifyTrackRepository;
        _normalizerService = normalizerService;
        _elasticSearchService = elasticSearchService;
    }

    public async Task<bool> BulkAsync(CancellationToken cancellationToken)
    {
        try
        {
            var spotifyPlays = _spotifyTrackRepository.GetSpotifyTrack();
            var spotifyPlayNormalized = _normalizerService.NormalizeMany(spotifyPlays);
            
            var batches = spotifyPlayNormalized
                .Chunk(BatchSize)
                .ToList();

            Console.WriteLine($"Starting bulk operation with {spotifyPlayNormalized.Count} documents in {batches.Count} batches");

            var result = true;
            var totalProcessed = 0;
            var totalErrors = 0;
            
            foreach (var batch in batches)
            {
                var bulkResponse = await _elasticSearchService.IndexDocumentsAsync(batch, "spotify_plays", cancellationToken);
                
                if (bulkResponse)
                {
                    totalProcessed += batch.Length;
                    Console.WriteLine($"Successfully processed batch: {batch.Length} documents. Total processed: {totalProcessed}");
                    result = false;
                }
                else
                {
                    totalErrors += batch.Length;
                    Console.WriteLine($"Error processing batch");
                    result = false;
                }
            }

            Console.WriteLine($"Bulk operation completed. Total processed: {totalProcessed}, Errors: {totalErrors}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during bulk operation: {ex.Message}");
            throw;
        }
    }
}