using System.Globalization;
using CsvHelper;
using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.BusinessService.LoadDataService;

public class LoadDataService : ILoadDataService
{
    public async Task IndexDataFromCsvAsync(CancellationToken cancellation)
    {
        var data = new List<SpotifyTrack>();
        try
        {
            using (var streamReader = new StreamReader("data.csv"))
            using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                data = csv.GetRecords<SpotifyTrack>().ToList();
            }
        }
        catch (Exception e)
        {
            
        }
    }
}