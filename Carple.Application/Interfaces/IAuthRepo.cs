using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Domain.Enities;

namespace Carple.Application.Interfaces
{
   public interface IAuthRepo
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
