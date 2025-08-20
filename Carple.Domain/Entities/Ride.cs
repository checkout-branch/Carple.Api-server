
namespace Carple.Domain.Entities;
public class Ride


{
    public int RideId { get; set; }
    public string RideType { get; set; }
    public int UserId { get; set; }
    public int CaptainId { get; set; }
    public int? VehicleId { get; set; }
    public int? PickupLocationId { get; set; }
    public int? DropLocationId { get; set; }
    public string RideStatus { get; set; }
    public DateTime? ScheduledTime { get; set; }
    public DateTime RideDate { get; set; }
    public int MaxPassengers { get; set; }
    public int CurrentPassengers { get; set; }
    public decimal EstimatedFare { get; set; }
    public decimal? ActualFare { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public decimal? PickupLatitude { get; set; }
    public decimal? PickupLongitude { get; set; }
    public string PickupAddress { get; set; }
    public string PickupPincode { get; set; }

    public decimal? DropLatitude { get; set; }
    public decimal? DropLongitude { get; set; }
    public string DropAddress { get; set; }
    public string DropPincode { get; set; }

    public double? DistanceInKm { get; set; }
}