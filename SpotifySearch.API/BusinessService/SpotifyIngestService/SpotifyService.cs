using Elastic.Clients.Elasticsearch;
using SpotifySearchAPI.BusinessService.ElasticsearchService;
using SpotifySearchAPI.Repository;

namespace SpotifySearchAPI.BusinessService.SpotifyIngestService;

public class SpotifyService : ISpotifyService
{
    private readonly ISpotifyTrackRepository _spotifyTrackRepository;
    private readonly IElasticsearchService _elasticsearchService;
    private readonly ElasticsearchClient _es;
    private const int BatchSize = 50; // Số lượng documents mỗi batch

    public SpotifyService(ISpotifyTrackRepository spotifyTrackRepository, IElasticsearchService elasticsearchService, ElasticsearchClient es)
    {
        _spotifyTrackRepository = spotifyTrackRepository;
        _elasticsearchService = elasticsearchService;
        _es = es;
    }

    public async Task<bool> BulkAsync(string index, CancellationToken cancellationToken)
    {
        try
        {
            var spotifyPlays = _spotifyTrackRepository.GetSpotifyTrack();
            
            var batches = spotifyPlays
                .Chunk(BatchSize)
                .ToList();

            var result = true;
            var totalProcessed = 0;
            var totalErrors = 0;
            
            foreach (var batch in batches)
            {
                var bulkResponse = await _elasticsearchService.IndexDocumentsAsync(batch, index, cancellationToken);
                
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