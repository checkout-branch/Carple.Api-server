using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Domain.Enities;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Repositories
{
    public interface IAuthRepo
    {
        Task<UserEntity> GetUserByEmailAsync(string email);
        Task<int> RegisterUserAsync(UserEntity user);
    }
}
