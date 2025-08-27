using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace SpotifySearchAPI.Model;

public class SpotifyPlay
{
    [JsonPropertyName("spotify_track_uri")]
    [Name("spotify_track_uri")]
    public string SpotifyTrackUri { get; set; }

    [JsonPropertyName("ts")]
    [Name("ts")]
    public DateTime TimeSheet { get; set; }

    [JsonPropertyName("track_name")]
    [Name("track_name")]
    public string TrackName { get; set; }

    [JsonPropertyName("artist_name")]
    [Name("artist_name")]
    public string ArtistName { get; set; }

    [JsonPropertyName("album_name")]
    [Name("album_name")]
    public string AlbumName { get; set; }

    [JsonPropertyName("platform")]
    [Name("platform")]
    public string Platform { get; set; }

    [JsonPropertyName("ms_played")]
    [Name("ms_played")]
    public int MsPlayed { get; set; }

    [JsonPropertyName("reason_start")]
    [Name("reason_start")]
    public string ReasonStart { get; set; }

    [JsonPropertyName("reason_end")]
    [Name("reason_end")]
    public string ReasonEnd { get; set; }

    [JsonPropertyName("shuffle")]
    [Name("shuffle")]
    public bool Shuffle { get; set; }

    [JsonPropertyName("skipped")]
    [Name("skipped")]
    public bool Skipped { get; set; }
}