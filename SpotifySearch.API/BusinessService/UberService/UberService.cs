using System.Globalization;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using SpotifySearchAPI.Dto.UberDto;
using SpotifySearchAPI.Model;
using SpotifySearchAPI.Repository.UberRepository;

namespace SpotifySearchAPI.BusinessService.UberService;

public class UberService : IUberService
{
    private readonly IRideBookingRepository _repository;
    private readonly ElasticsearchClient _es;
    private readonly string _indexName = "ride-bookings";
    private readonly string _pipeline = "ride-bookings-pipeline";
    public UberService(IRideBookingRepository repository, ElasticsearchClient es)
    {
        _repository = repository;
        _es = es;
    }

    public async Task<bool> BulkAsync(CancellationToken cancellationToken)
    {
        try
        {
            var rideBookings = _repository.GetRideBooking();
            var documents = ParseToDocument(rideBookings);
            
            var batches = documents
                .Chunk(500)
                .ToList();

            var result = true;
            var totalProcessed = 0;
            var totalErrors = 0;

            foreach (var batch in batches)
            {
                var bulkResponse = await PushRideBookingDocumentAsync(batch, cancellationToken);
                
                if (bulkResponse)
                {
                    totalProcessed += batch.Length;
                    Console.WriteLine($"Successfully processed batch: {batch.Length} documents. Total processed: {totalProcessed}");
                    result = false;
                }
                else
                {
                    totalErrors += batch.Length;
                    Console.WriteLine($"Error processing batch");
                    result = false;
                }
            }
            
            Console.WriteLine($"Bulk operation completed. Total processed: {totalProcessed}, Errors: {totalErrors}");
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error during bulk operation: {e.Message}");
            throw;
        }
    }

    private List<UberDocument> ParseToDocument(List<RideBookingCsv> rideBookings)
    {
        var documents = new List<UberDocument>();
        foreach (var rideBooking in rideBookings)
        {
            var document = new UberDocument
            {
                Time = rideBooking.Time,
                BookingId = rideBooking.BookingId,
                BookingStatus = rideBooking.BookingStatus,
                CustomerId = rideBooking.CustomerId,
                VehicleType = rideBooking.VehicleType,
                PickupLocation = rideBooking.PickupLocation,
                DropLocation = rideBooking.DropLocation,
                AvgVTAT = rideBooking.AvgVTAT != "null" ? double.Parse(rideBooking.AvgVTAT, CultureInfo.InvariantCulture) : 0,
                AvgCTAT = rideBooking.AvgCTAT != "null" ? double.Parse(rideBooking.AvgCTAT, CultureInfo.InvariantCulture) : 0,
                CancelledByCustomer = rideBooking.CancelledByCustomer != "null",
                CustomerCancellationReason = rideBooking.CustomerCancellationReason != "null" ? rideBooking.CustomerCancellationReason : "",
                CancelledByDriver = rideBooking.CancelledByDriver != "null",
                DriverCancellationReason = rideBooking.DriverCancellationReason != "null" ? rideBooking.DriverCancellationReason : "",
                IncompleteRides = rideBooking.IncompleteRides != "null",
                IncompleteRidesReason = rideBooking.IncompleteRidesReason != "null" ? rideBooking.IncompleteRidesReason : "",
                BookingValue = rideBooking.BookingValue != "null" ? int.Parse(rideBooking.BookingValue, CultureInfo.InvariantCulture) : 0,
                RideDistance = rideBooking.RideDistance != "null" ? double.Parse(rideBooking.RideDistance, CultureInfo.InvariantCulture) : 0,
                DriverRatings = rideBooking.DriverRatings != "null" ? double.Parse(rideBooking.DriverRatings, CultureInfo.InvariantCulture) : 0,
                CustomerRating = rideBooking.CustomerRating != "null" ? double.Parse(rideBooking.CustomerRating, CultureInfo.InvariantCulture) : 0,
                PaymentMethod = rideBooking.PaymentMethod != "null" ? rideBooking.PaymentMethod : "",
            };
            
            if (DateOnly.TryParseExact(rideBooking.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d))
            {
                document.Date = d.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            
            documents.Add(document);
        }
    
        return documents;
    }

    private async Task<bool> PushRideBookingDocumentAsync(IEnumerable<UberDocument> rideBookings, CancellationToken cancellationToken)
    {
        try
        {
            var bulkRequest = new BulkRequest(_indexName)
            {
                Operations = rideBookings.Select(doc => new BulkCreateOperation<UberDocument>(doc)
                {
                    Pipeline = _pipeline
                }).Cast<IBulkOperation>().ToList()
            };
            var response = await _es.BulkAsync(bulkRequest, cancellationToken);
            return response.IsValidResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        
    }
}