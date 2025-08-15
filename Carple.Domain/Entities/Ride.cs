using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Domain.Entities
{
    public class Ride
    {
        public int RideId { get; set; }
        public int UserId { get; set; }
        public int CaptainId { get; set; }
        public decimal ActualFare { get; set; }
    }
}
