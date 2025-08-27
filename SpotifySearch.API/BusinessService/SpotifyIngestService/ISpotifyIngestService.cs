namespace SpotifySearchAPI.BusinessService.SpotifyIngestService;

public interface ISpotifyIngestService
{
    Task<bool> BulkAsync(CancellationToken cancellationToken);
}