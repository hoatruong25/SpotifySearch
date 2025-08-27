namespace SpotifySearchAPI.BusinessService.ElasticsearchService;

public interface IElasticSearchService
{
    Task<bool> IndexDocumentsAsync<T>(IEnumerable<T> documents, string indexName, CancellationToken cancellation)
        where T : class;
}