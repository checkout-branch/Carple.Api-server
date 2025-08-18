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
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ApiResponse<string>> CreateVehicleAsync(Vehicle vehicle)
        {
            await _vehicleRepository.CreateAsync(vehicle);
            return new ApiResponse<string>(true, "Vehicle created successfully", null);
        }
        public async Task<ApiResponse<IEnumerable<Vehicle>>> GetAllVehiclesAsync()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            return new ApiResponse<IEnumerable<Vehicle>>(true, "Vehicles retrieved successfully", vehicles);
        }
        public async Task<ApiResponse<Vehicle?>> GetVehicleByIdAsync(int vehicleId)
        {
            if (vehicleId <= 0) return new ApiResponse<Vehicle?>(false, "Invalid VehicleId", null);
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle == null) return new ApiResponse<Vehicle?>(false, "Vehicle not found", null);
            return new ApiResponse<Vehicle?>(true, "Vehicle retrieved successfully", vehicle);
        }
        public async Task<ApiResponse<string>> UpdateVehicleAsync(Vehicle vehicle)
        {
            if (vehicle == null || vehicle.VehicleId <= 0) return new ApiResponse<string>(false, "Invalid Vehicle data", null);
            await _vehicleRepository.UpdateAsync(vehicle);
            return new ApiResponse<string>(true, "Vehicle updated successfully", null);
        }

        public async Task<ApiResponse<string>> DeleteVehicleAsync(int vehicleId)
        {
            if (vehicleId <= 0)
                return new ApiResponse<string>(false, "Invalid VehicleId", null);

            await _vehicleRepository.DeleteAsync(vehicleId);
            return new ApiResponse<string>(true, "Vehicle deleted successfully", null);
        }
        public async Task<ApiResponse<IEnumerable<Vehicle>>> GetVehiclesByCaptainIdAsync(int captainId)
        {
            if (captainId <= 0)
                return new ApiResponse<IEnumerable<Vehicle>>(false, "Invalid CaptainId", null);

            var vehicles = await _vehicleRepository.GetByCaptainIdAsync(captainId);
            return new ApiResponse<IEnumerable<Vehicle>>(true, "Vehicles retrieved successfully", vehicles);
        }

        public async Task<ApiResponse<string>> MarkVehicleAsVerifiedAsync(int vehicleId)
        {
            if (vehicleId <= 0)
                return new ApiResponse<string>(false, "Invalid VehicleId", null);

            await _vehicleRepository.MarkAsVerifiedAsync(vehicleId);
            return new ApiResponse<string>(true, "Vehicle marked as verified successfully", null);
        }
    }
}
