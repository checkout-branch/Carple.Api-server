using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;

namespace Carple.Application.Interfaces.Services
{
    public interface IRideJoinRequestService
    {
        Task<ApiResponse<int>> CreateRequestAsync(RideJoinRequest request);
        Task<ApiResponse<IEnumerable<RideJoinRequest>>> GetRequestsByRideIdAsync(int rideId);
        Task<ApiResponse<string>> UpdateRequestStatusAsync(int requestId, string status);
    }
}
