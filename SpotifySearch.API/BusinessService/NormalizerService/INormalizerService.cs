using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.BusinessService.NormalizerService;

public interface INormalizerService
{
    List<SpotifyPlayNormalized> NormalizeMany(IEnumerable<SpotifyPlayRaw> rows);
    SpotifyPlayNormalized? NormalizeOne(SpotifyPlayRaw raw);
}