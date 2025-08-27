using System.Text.Json.Serialization;

namespace SpotifySearchAPI.Model;

public class SpotifyPlayNormalized
{
    [JsonPropertyName("spotify_track_uri")]
    public string SpotifyTrackUri { get; set; }
    [JsonPropertyName("ts")]
    public DateTime TimeSheet { get; set; }
    
    [JsonPropertyName("track_name")]
    public string TrackName { get; set; }
    
    [JsonPropertyName("artist_name")]
    public string ArtistName { get; set; }
    
    [JsonPropertyName("album_name")]
    public string AlbumName { get; set; }
    
    [JsonPropertyName("platform")]
    public string Platform { get; set; }
    
    [JsonPropertyName("ms_played")]
    public int MsPlayed { get; set; }
    
    [JsonPropertyName("reason_start")]
    public string ReasonStart { get; set; }
    
    [JsonPropertyName("reason_end")]
    public string ReasonEnd { get; set; }
    
    [JsonPropertyName("shuffle")]
    public bool Shuffle { get; set; }
    
    [JsonPropertyName("skipped")]
    public bool Skipped { get; set; }
}