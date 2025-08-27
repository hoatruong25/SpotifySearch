using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;

namespace SpotifySearchAPI.BusinessService.ElasticsearchService;

public class ElasticSearchService : IElasticSearchService
{
    private readonly ElasticsearchClient _es;
    public ElasticSearchService(ElasticsearchClient es)
    {
        _es = es;
    }

    public async Task<bool> IndexDocumentsAsync<T>(IEnumerable<T> documents, string indexName, CancellationToken cancellation) where T : class
    {
        try
        {
            var bulkRequest = new BulkRequest
            {
                Operations = documents.Select(doc => new BulkCreateOperation<T>(doc)
                {
                    Index = indexName,
                }).Cast<IBulkOperation>().ToList()
            };

            var response = await _es.BulkAsync(bulkRequest, cancellation);
            return response.IsValidResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public async Task InsertDocumentAsync<T>(T document, string indexName, CancellationToken cancellation) where T : class
    {
        await _es.IndexAsync(document, idx => idx.Index(indexName), cancellation);
    }
}