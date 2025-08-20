using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Services
{
 public interface IPersonalDriverService
    {
        Task<ApiResponse<IEnumerable<Ride>>> GetAllRidesAsync();
        Task<ApiResponse<int>> CreateRideAsync(Ride ride);
        Task<ApiResponse<string>> UpdateRideStatusAsync(int rideId, string status);
        Task<ApiResponse<Ride?>> GetRideByIdAsync(int rideId);
    }
}
