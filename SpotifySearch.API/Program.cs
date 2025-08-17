using Scalar.AspNetCore;
using SpotifySearchAPI.Registrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

/* Auto-register all services and repositories */
builder.Services
    .AddServiceRegistrations()
    .AddElasticsearchRegistration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();