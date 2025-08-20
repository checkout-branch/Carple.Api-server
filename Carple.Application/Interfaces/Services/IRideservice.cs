using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Services
{
   public interface IRideservice
    {
        Task<ApiResponse<string>> CreateRideAsync(Ride ride);
        Task<ApiResponse<string>> UpdateRideStatusAsync(int rideId, string rideStatus);
        Task<ApiResponse<string>> DeleteRideAsync(int rideId);
        Task<ApiResponse<Ride?>> GetRideByIdAsync(int rideId);
        Task<ApiResponse<IEnumerable<Ride>>> GetAllRidesAsync();

        

    }
}
