using System.Globalization;
using CsvHelper;
using SpotifySearchAPI.Model;
using SpotifySearchAPI.Repository.SpotifyRepository;

namespace SpotifySearchAPI.Repository;

public class SpotifyTrackRepository : ISpotifyTrackRepository
{
    public List<SpotifyPlay> GetSpotifyTrack()
    {
        using var streamReader = new StreamReader("Dataset/spotify/spotify_history.csv");
        using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        return csv.GetRecords<SpotifyPlay>().ToList();
    }
}