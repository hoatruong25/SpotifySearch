using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace SpotifySearchAPI.Dto.UberDto;

public class UberDocument
{
    [JsonPropertyName("Date")]
    public string Date { get; set; }
    
    [JsonPropertyName("Time")]
    public string Time { get; set; }
    
    [JsonPropertyName("Booking ID")]
    public string BookingId { get; set; }
    
    [JsonPropertyName("Booking Status")]
    public string BookingStatus { get; set; }
    
    [JsonPropertyName("Customer ID")]
    public string CustomerId { get; set; }
    
    [JsonPropertyName("Vehicle Type")]
    public string VehicleType { get; set; }
    
    [JsonPropertyName("Pickup Location")]
    public string PickupLocation { get; set; }
    
    [JsonPropertyName("Drop Location")]
    public string DropLocation { get; set; }
    
    [JsonPropertyName("Avg VTAT")]
    public double AvgVTAT { get; set; }
    
    [JsonPropertyName("Avg CTAT")]
    public double AvgCTAT { get; set; }
    
    [JsonPropertyName("Cancelled Rides by Customer")]
    public bool CancelledByCustomer { get; set; }
    
    [JsonPropertyName("Reason for cancelling by Customer")]
    public string CustomerCancellationReason { get; set; }
    
    [JsonPropertyName("Cancelled Rides by Driver")]
    public bool CancelledByDriver { get; set; }
    
    [JsonPropertyName("Driver Cancellation Reason")]
    public string DriverCancellationReason { get; set; }
    
    [JsonPropertyName("Incomplete Rides")]
    public bool IncompleteRides { get; set; }
    
    [JsonPropertyName("Incomplete Rides Reason")]
    public string IncompleteRidesReason { get; set; }
    
    [JsonPropertyName("Booking Value")]
    public int BookingValue { get; set; } = 0;
    
    [JsonPropertyName("Ride Distance")]
    public double RideDistance { get; set; } = 0;
    
    [JsonPropertyName("Driver Ratings")]
    public double DriverRatings { get; set; } = 0;
    
    [JsonPropertyName("Customer Rating")]
    public double CustomerRating { get; set; } = 0;
    
    [JsonPropertyName("Payment Method")]
    public string PaymentMethod { get; set; }
}