using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.BusinessService.SpotifyIngestService;

public interface ISpotifyService
{
    Task<bool> BulkAsync(string index, CancellationToken cancellationToken);

    Task<List<SpotifyPlay>> SearchFullTextAsync(string indexName, string fieldName, string query, int size,
        CancellationToken cancellation);
}