namespace SpotifySearchAPI.BusinessService.UberService;

public interface IUberService
{
    Task<bool> BulkAsync(CancellationToken cancellationToken);
}