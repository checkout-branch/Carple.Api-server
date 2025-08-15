using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int RideId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public decimal CompanyShare { get; set; }
        public decimal CaptainShare { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public DateTime PaidAt { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Address { get; set; }
        public string? Pincode { get; set; }
    }
}
