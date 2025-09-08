using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace SpotifySearchAPI.Model;

public class RideBookingCsv
{
    [Name("Date")]
    public string Date { get; set; }

    [Name("Time")]
    public string Time { get; set; }

    [Name("Booking ID")]
    public string BookingId { get; set; }

    [Name("Booking Status")]
    public string BookingStatus { get; set; }

    [Name("Customer ID")]
    public string CustomerId { get; set; }

    [Name("Vehicle Type")]
    public string VehicleType { get; set; }

    [Name("Pickup Location")]
    public string PickupLocation { get; set; }

    [Name("Drop Location")]
    public string DropLocation { get; set; }

    [Name("Avg VTAT")]
    public string AvgVTAT { get; set; }

    [Name("Avg CTAT")]
    public string AvgCTAT { get; set; }

    [Name("Cancelled Rides by Customer")]
    public string CancelledByCustomer { get; set; }

    [Name("Reason for cancelling by Customer")]
    public string CustomerCancellationReason { get; set; }

    [Name("Cancelled Rides by Driver")]
    public string CancelledByDriver { get; set; }

    [Name("Driver Cancellation Reason")]
    public string DriverCancellationReason { get; set; }

    [Name("Incomplete Rides")]
    public string IncompleteRides { get; set; }

    [Name("Incomplete Rides Reason")]
    public string IncompleteRidesReason { get; set; }

    [Name("Booking Value")]
    public string BookingValue { get; set; }

    [Name("Ride Distance")]
    public string RideDistance { get; set; }

    [Name("Driver Ratings")]
    public string DriverRatings { get; set; }

    [Name("Customer Rating")]
    public string CustomerRating { get; set; }

    [Name("Payment Method")]
    public string PaymentMethod { get; set; }
}