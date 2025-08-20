using Carple.Domain.Entities;


    public class RideJoinRequest
    {
        public int RequestId { get; set; }        // Primary key
        public int RideId { get; set; }           // The ride the user wants to join
        public int UserId { get; set; }           // ID of the user sending the request
        public int RequestedSeats { get; set; }   // Number of seats requested
        public string RequestStatus { get; set; } // Status: Pending, Accepted, Rejected
        public DateTime RequestedAt { get; set; } // Timestamp of the request
    }

