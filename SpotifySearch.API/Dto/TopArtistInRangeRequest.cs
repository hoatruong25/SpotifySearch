namespace SpotifySearchAPI.Dto;

public class TopArtistInRangeRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Size { get; set; }
}