namespace SpotifySearchAPI.BusinessService.SpotifyIngestService;

public interface ISpotifyService
{
    Task<bool> BulkAsync(string index, CancellationToken cancellationToken);
}