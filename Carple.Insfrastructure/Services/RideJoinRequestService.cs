using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Application.Interfaces.Repositories;
using Carple.Application.Interfaces.Services;

namespace Carple.Insfrastructure.Services
{
    public class RideJoinRequestService : IRideJoinRequestService
    {
        private readonly IRideJoinRequestRepository _repository;
        public RideJoinRequestService(IRideJoinRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<int>> CreateRequestAsync(RideJoinRequest request)
        {
            var requestId = await _repository.CreateAsync(request);
            return new ApiResponse<int>(true, "Request created successfully", requestId);
        }

        public async Task<ApiResponse<IEnumerable<RideJoinRequest>>> GetRequestsByRideIdAsync(int rideId)
        {
            var requests = await _repository.GetByRideIdAsync(rideId);
            return new ApiResponse<IEnumerable<RideJoinRequest>>(true, "Requests retrieved successfully", requests);
        }

        public async Task<ApiResponse<string>> UpdateRequestStatusAsync(int requestId, string status)
        {
            var result = await _repository.UpdateStatusAsync(requestId, status);
            return new ApiResponse<string>(true, "Request status updated", result);
        }
    }
}
