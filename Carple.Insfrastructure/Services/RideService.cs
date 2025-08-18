using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Application.Interfaces.Repositories;
using Carple.Application.Interfaces.Services;
using Carple.Domain.Entities;

namespace Carple.Insfrastructure.Services
{
    public class RideService : IRideservice
    {
        private readonly IRideRepository _rideRepository;
        public RideService(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
        }
        public async Task<ApiResponse<string>> CreateRideAsync(Ride ride)
        {
            await _rideRepository.CreateAsync(ride);
            return new ApiResponse<string>(true, "Ride created successfully", null);
        }

        public async Task<ApiResponse<string>> UpdateRideStatusAsync(int rideId, string rideStatus)
        {
            await _rideRepository.UpdateAsync(rideId, rideStatus);
            return new ApiResponse<string>(true, "Ride status updated successfully", null);
        }

        public async Task<ApiResponse<string>> DeleteRideAsync(int rideId)
        {
            await _rideRepository.DeleteAsync(rideId);
            return new ApiResponse<string>(true, "Ride deleted successfully", null);
        }

        public async Task<ApiResponse<Ride?>> GetRideByIdAsync(int rideId)
        {
            var ride = await _rideRepository.GetByIdAsync(rideId);
            if (ride == null) return new ApiResponse<Ride?>(false, "Ride not found", null);

            return new ApiResponse<Ride?>(true, "Ride retrieved successfully", ride);
        }

        public async Task<ApiResponse<IEnumerable<Ride>>> GetAllRidesAsync()
        {
            var rides = await _rideRepository.GetAllAsync();
            return new ApiResponse<IEnumerable<Ride>>(true, "Rides retrieved successfully", rides);
        }

    }
}
