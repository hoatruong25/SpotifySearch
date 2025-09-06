namespace SpotifySearchAPI.BusinessService.ElasticsearchService;

public interface IElasticsearchService
{
    Task<bool> IndexDocumentsAsync<T>(IEnumerable<T> documents, string indexName, CancellationToken cancellation)
        where T : class;

    Task<bool> InsertDocumentAsync<T>(T document, string indexName, CancellationToken cancellation)
        where T : class;

    Task<List<T>> SearchFullTextAsync<T>(string indexName, string fieldName, string query, int size,
        CancellationToken cancellation) where T : class;
}