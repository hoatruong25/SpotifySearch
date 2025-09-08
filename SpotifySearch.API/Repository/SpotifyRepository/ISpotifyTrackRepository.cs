using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.Repository.SpotifyRepository;

public interface ISpotifyTrackRepository
{
    List<SpotifyPlay> GetSpotifyTrack();
}