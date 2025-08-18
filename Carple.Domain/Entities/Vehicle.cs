using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int OwnerId { get; set; }
        public string VehicleNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string RCBook { get; set; }
        public string InsuranceCopy { get; set; }
        public bool IsVerified { get; set; }
        public int? CaptainId { get; set; }
        public string Color { get; set; }
        public int SeatingCapacity { get; set; }
        public string Features { get; set; }
        public string VehicleImage { get; set; }
    }
}
