using Scalar.AspNetCore;
using SpotifySearchAPI.BusinessService.LoadDataService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

/* DI Business Service */
builder.Services.AddScoped<ILoadDataService, LoadDataService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
