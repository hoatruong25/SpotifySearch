using CsvHelper.Configuration.Attributes;

namespace SpotifySearchAPI.Model;

public class SpotifyTrack
{
    [Name("spotify_track_uri")]
    public string SpotifyTrackUri { get; set; }
    [Name("ts")]
    public string TimeSheet { get; set; }
    [Name("platform")]
    public string Platform { get; set; }
    [Name("ms_played")]
    public int MsPlayed { get; set; }
    [Name("track_name")]
    public string TrackName { get; set; }
    [Name("artist_name")]
    public string ArtistName { get; set; }
    [Name("album_name")]
    public string AlbumName { get; set; }
    [Name("reason_start")]
    public string ReasonStart { get; set; }
    [Name("reason_end")]
    public string ReasonEnd { get; set; }
    [Name("shuffle")]
    public bool Shuffle { get; set; }
    [Name("skipped")]
    public bool Skipped { get; set; }
}
