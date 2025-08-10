using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Scalar.AspNetCore;
using SpotifySearchAPI.BusinessService.LoadDataService;
using SpotifySearchAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

/* Register Elasticsearch service */
var elasticUri = builder.Configuration["Elasticsearch:Uri"];
var username   = builder.Configuration["Elasticsearch:Username"];
var password   = builder.Configuration["Elasticsearch:Password"];
var settings = new ElasticsearchClientSettings(new Uri(elasticUri))
    .Authentication(new BasicAuthentication(username, password))
    .DefaultIndex("default-index") // optional
    .PrettyJson()
    .RequestTimeout(TimeSpan.FromMinutes(2));

/* DI Business Service */
builder.Services.AddScoped<ILoadDataService, LoadDataService>();

/* DI Repository */
builder.Services.AddScoped<ISpotifyTrackRepository, SpotifyTrackRepository>();

/* DI 3rd party service */
builder.Services.AddSingleton(new ElasticsearchClient(settings));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
