using Elastic.Clients.Elasticsearch;

namespace SpotifySearchAPI.BusinessService.ElasticsearchService;

public interface IElasticSearchService
{
    Task<bool> IndexDocumentsAsync<T>(IEnumerable<T> documents, string indexName, CancellationToken cancellation)
        where T : class;

    Task<bool> InsertDocumentAsync<T>(T document, string indexName, CancellationToken cancellation)
        where T : class;
}