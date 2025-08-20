using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<ApiResponse<string>> CreateVehicleAsync(Vehicle vehicle);
        Task<ApiResponse<IEnumerable<Vehicle>>> GetAllVehiclesAsync();
        Task<ApiResponse<Vehicle?>> GetVehicleByIdAsync(int vehicleId);
        Task<ApiResponse<string>> UpdateVehicleAsync(Vehicle vehicle);
        Task<ApiResponse<IEnumerable<Vehicle>>> GetVehiclesByCaptainIdAsync(int captainId);
        Task<ApiResponse<string>> MarkVehicleAsVerifiedAsync(int vehicleId);
        Task<ApiResponse<string>> DeleteVehicleAsync(int vehicleId);

    }
}
