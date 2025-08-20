using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        Task<int> CreateAsync(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<Vehicle?> GetByIdAsync(int vehicleId);
        Task<int> UpdateAsync(Vehicle vehicle);
        Task<int> DeleteAsync(int vehicleId);
        Task<IEnumerable<Vehicle>> GetByCaptainIdAsync(int captainId);
        Task<int> MarkAsVerifiedAsync(int vehicleId);
    }
}