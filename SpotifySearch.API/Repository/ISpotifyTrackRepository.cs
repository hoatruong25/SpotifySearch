using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.Repository;

public interface ISpotifyTrackRepository
{
    List<SpotifyPlay> GetSpotifyTrack();
}