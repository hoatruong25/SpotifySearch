using SpotifySearchAPI.Model;

namespace SpotifySearchAPI.Repository.UberRepository;

public interface IRideBookingRepository
{
    List<RideBookingCsv> GetRideBooking();
}