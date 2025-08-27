using System.Globalization;
using CsvHelper;
using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.Repository;

public class SpotifyTrackRepository : ISpotifyTrackRepository
{
    public List<SpotifyPlay> GetSpotifyTrack()
    {
        using var streamReader = new StreamReader("Dataset/spotify_history.csv");
        using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        return csv.GetRecords<SpotifyPlay>().ToList();
    }
}