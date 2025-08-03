using System.Globalization;
using CsvHelper;
using SpotifySearchAPI.Model;
using SpotifySearchAPI.Repository;

namespace SpotifySearchAPI.BusinessService.LoadDataService;

public class LoadDataService : ILoadDataService
{
    private readonly ISpotifyTrackRepository _spotifyTrackRepository;

    public LoadDataService(ISpotifyTrackRepository spotifyTrackRepository)
    {
        _spotifyTrackRepository = spotifyTrackRepository;
    }

    public async Task IndexDataFromCsvAsync(CancellationToken cancellation)
    {
        var data = _spotifyTrackRepository.GetSpotifyTrack();
    }
}