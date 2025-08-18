using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Application.Interfaces.Repositories
{
    public interface IRideJoinRequestRepository
    {
        Task<int> CreateAsync(RideJoinRequest request);                // FLAG = 1
        Task<IEnumerable<RideJoinRequest>> GetByRideIdAsync(int rideId); // FLAG = 2
        Task<string> UpdateStatusAsync(int requestId, string status);  // FLAG = 3
    }
}
