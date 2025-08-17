using System.Globalization;
using System.Text.RegularExpressions;
using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.BusinessService.NormalizerService;

public class NormalizerService : INormalizerService
{
    private static readonly Regex MultiSpace = new(@"\s+", RegexOptions.Compiled);
    
    public List<SpotifyPlayNormalized> NormalizeMany(IEnumerable<SpotifyPlayRaw> rows)
        => rows.Select(NormalizeOne).Where(x => x != null).Cast<SpotifyPlayNormalized>().ToList();
    
    public SpotifyPlayNormalized? NormalizeOne(SpotifyPlayRaw raw)
    {
        var trackName = CleanString(raw.TrackName);
        var artistName = CleanString(raw.ArtistName);
        var albumName = CleanString(raw.AlbumName);
        var platform = CleanString(raw.Platform);
        var reasonStart = CleanString(raw.ReasonStart);
        var reasonEnd = CleanString(raw.ReasonEnd);
                    
        // TrackName and ArtistName is required
        if (string.IsNullOrEmpty(trackName) || string.IsNullOrEmpty(artistName)) 
            return null;
        
        var timeSheet = DateTime.ParseExact(raw.TimeSheet, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        
        return new SpotifyPlayNormalized
        {
            SpotifyTrackUri = raw.SpotifyTrackUri,
            TimeSheet = timeSheet,
            Platform = platform ?? "",
            MsPlayed = raw.MsPlayed,
            TrackName = trackName,
            ArtistName = artistName,
            AlbumName = albumName ?? "",
            ReasonStart = reasonStart ?? "",
            ReasonEnd = reasonEnd ?? "",
            Shuffle = raw.Shuffle,
            Skipped = raw.Skipped
        };
    }
    
    private string? CleanString(string? s)
    {
        if (string.IsNullOrWhiteSpace(s)) return null;
        s = s.Trim();
        s = MultiSpace.Replace(s, " ");
        return s;
    }
}