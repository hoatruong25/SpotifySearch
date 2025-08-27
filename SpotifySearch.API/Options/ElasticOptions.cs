namespace SpotifySearchAPI.Options;

public sealed class ElasticOptions
{
    public string[] Urls { get; set; } = [];
    public string? DefaultIndex { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int RequestTimeoutSeconds { get; set; } = 30;
    public bool EnableDebugMode { get; set; } = false;
    public bool SkipSslValidation { get; set; } = true; // Mặc định bỏ qua SSL cho development
}