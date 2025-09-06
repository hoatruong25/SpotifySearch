
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;

namespace SpotifySearchAPI.BusinessService.ElasticsearchService;

public class ElasticsearchService : IElasticsearchService
{
    private readonly ElasticsearchClient _es;
    public ElasticsearchService(ElasticsearchClient es)
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
    
    public async Task<bool> InsertDocumentAsync<T>(T document, string indexName, CancellationToken cancellation) where T : class
    {
        var result = await _es.IndexAsync(document, idx => idx.Index(indexName), cancellation);
        return result.IsSuccess();
    }

    public async Task<List<T>> SearchFullTextAsync<T>(string indexName, string fieldName, string query, int size,
        CancellationToken cancellation) where T : class
    {
        var res = await _es.SearchAsync<T>(s => s
            .Indices(indexName)
            .Query(q => q.Match(m => m
                .Field(ff => ff.GetType().Name == fieldName)
                .Query(query)
                .Fuzziness("AUTO")
            ))
            .Size(size), cancellation);

        return res.Documents.ToList();
    }
}