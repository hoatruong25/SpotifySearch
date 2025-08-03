namespace SpotifySearchAPI.Model;

public class SpotifyTrack
{
    public string Id { get; set; }
    public DateTime ListenTime { get; set; }
    public string UserId { get; set; }
    public string Platform { get; set; }
    public string SongName { get; set; }
    public string Album { get; set; }
    public string Artist { get; set; }
    public string ReasonStart { get; set; }
    public string ReasonEnd { get; set; }
    public bool Shuffle { get; set; }
    public bool Skipped { get; set; }
}