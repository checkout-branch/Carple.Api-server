using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Repositories
{
   public interface IRideRepository
    {
        Task<int> CreateAsync(Ride ride);
        Task<int> UpdateAsync(int rideId, string rideStatus);
        Task<int> DeleteAsync(int rideId);
        Task<Ride?> GetByIdAsync(int rideId);

        Task<IEnumerable<Ride>> GetAllAsync();

    }
}
