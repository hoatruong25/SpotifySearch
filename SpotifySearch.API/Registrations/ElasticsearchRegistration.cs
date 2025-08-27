using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using SpotifySearchAPI.Options;

namespace SpotifySearchAPI.Registrations;

public static class ElasticsearchRegistration
{
    public static IServiceCollection AddElasticsearchRegistration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ElasticOptions>(configuration.GetSection("Elastic"));

        services.AddSingleton<ElasticsearchClient>(sp =>
        {
            var opt = sp.GetRequiredService<IOptions<ElasticOptions>>().Value;

            ElasticsearchClientSettings settings;

            // Self-hosted (Docker, on-prem)
            if (opt.Urls is { Length: > 1 })
            {
                // Multi-node: StaticNodePool để client tự round-robin
                var uris = opt.Urls.Select(u => new Uri(u));
                var pool = new StaticNodePool(uris);
                settings = new ElasticsearchClientSettings(pool);
            }
            else
            {
                settings = new ElasticsearchClientSettings(new Uri(opt.Urls.First()));
            }

            settings = settings.Authentication(new BasicAuthentication(opt.Username!, opt.Password!));

            if (!string.IsNullOrWhiteSpace(opt.DefaultIndex))
                settings = settings.DefaultIndex(opt.DefaultIndex);

            settings = settings.RequestTimeout(TimeSpan.FromSeconds(opt.RequestTimeoutSeconds));

            if (opt.EnableDebugMode)
                settings = settings.EnableDebugMode();

            // Bỏ qua SSL certificate validation nếu được cấu hình
            if (opt.SkipSslValidation)
                settings = settings.ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true);

            return new ElasticsearchClient(settings);
        });

        // Ping để kiểm tra kết nối
        using var serviceProvider = services.BuildServiceProvider();
        var client = serviceProvider.GetRequiredService<ElasticsearchClient>();
        var options = serviceProvider.GetRequiredService<IOptions<ElasticOptions>>().Value;
        try
        {
            var pingResponse = client.Ping();
            if (pingResponse.IsValidResponse)
            {
                Console.WriteLine($"✅ Successfully connected to Elasticsearch at {string.Join(", ", options.Urls)}");
            }
            else
            {
                Console.WriteLine($"❌ Failed to connect to Elasticsearch: {pingResponse.ElasticsearchServerError?.Error}");
                Console.WriteLine($"Debug info: {pingResponse.DebugInformation}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Exception while connecting to Elasticsearch: {ex.Message}");
        }
        
        return services;
    }
}