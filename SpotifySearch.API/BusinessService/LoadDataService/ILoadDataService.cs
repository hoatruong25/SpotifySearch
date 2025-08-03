namespace SpotifySearchAPI.BusinessService.LoadDataService;

public interface ILoadDataService
{
    Task IndexDataFromCsvAsync(CancellationToken cancellation);
}