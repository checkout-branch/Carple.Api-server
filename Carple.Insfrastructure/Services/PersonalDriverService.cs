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
   public class PersonalDriverService:IPersonalDriverService
    {
        private readonly IPersonalDriverRepository _repository;

        public PersonalDriverService(IPersonalDriverRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<IEnumerable<Ride>>> GetAllRidesAsync()
        {
            var rides = await _repository.GetAllAsync();
            return new ApiResponse<IEnumerable<Ride>>(true, "Rides retrieved", rides);
        }

        public async Task<ApiResponse<int>> CreateRideAsync(Ride ride)
        {
            var rideId = await _repository.CreateAsync(ride);
            return new ApiResponse<int>(true, "Ride created successfully", rideId);
        }

        public async Task<ApiResponse<string>> UpdateRideStatusAsync(int rideId, string status)
        {
            await _repository.UpdateStatusAsync(rideId, status);
            return new ApiResponse<string>(true, "Ride status updated", null);
        }

        public async Task<ApiResponse<Ride?>> GetRideByIdAsync(int rideId)
        {
            var ride = await _repository.GetByIdAsync(rideId);
            if (ride == null)
                return new ApiResponse<Ride?>(false, "Ride not found", null);
            return new ApiResponse<Ride?>(true, "Ride retrieved", ride);
        }
    }
}
