namespace SpotifySearchAPI.Model;

public class SpotifyPlayNormalized
{
    public string SpotifyTrackUri { get; set; }
    public DateTime TimeSheet { get; set; }
    public string Platform { get; set; }
    public int MsPlayed { get; set; }
    public string TrackName { get; set; }
    public string ArtistName { get; set; }
    public string AlbumName { get; set; }
    public string ReasonStart { get; set; }
    public string ReasonEnd { get; set; }
    public bool Shuffle { get; set; }
    public bool Skipped { get; set; }
}