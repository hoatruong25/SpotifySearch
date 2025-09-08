using System.Globalization;
using CsvHelper;
using SpotifySearchAPI.Model;
using SpotifySearchAPI.Repository.UberRepository;

namespace SpotifySearchAPI.Repository;

public class RideBookingRepository : IRideBookingRepository
{
    public List<RideBookingCsv> GetRideBooking()
    {
        using var streamReader = new StreamReader("Dataset/uber/ncr_ride_bookings.csv");
        using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        return csv.GetRecords<RideBookingCsv>().ToList();
    }
}