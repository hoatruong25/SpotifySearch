using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace SpotifySearchAPI.Registrations;

public static class ElasticsearchRegistration
{
    public static IServiceCollection AddElasticsearchRegistration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var elasticUri = configuration["Elasticsearch:Uri"];
        var username   = configuration["Elasticsearch:Username"];
        var password   = configuration["Elasticsearch:Password"];
        var settings = new ElasticsearchClientSettings(new Uri(elasticUri))
            .Authentication(new BasicAuthentication(username, password))
            .DefaultIndex("default-index") // optional
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(2));
        
        services.AddSingleton(new ElasticsearchClient(settings));

        return services;
    }
}