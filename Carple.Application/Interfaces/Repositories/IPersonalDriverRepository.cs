using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Repositories
{
   public interface IPersonalDriverRepository
    {
        Task<IEnumerable<Ride>> GetAllAsync();
        Task<int> CreateAsync(Ride ride);
        Task UpdateStatusAsync(int rideId, string status);
        Task<Ride?> GetByIdAsync(int rideId);
    }
}
