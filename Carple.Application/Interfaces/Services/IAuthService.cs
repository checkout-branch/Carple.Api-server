using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Dto;
using Carple.Domain.Enities;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto dto);

        Task<UserEntity?> GetUserByEmailAsync(string email);
        Task<string> RegisterAsync(RegisterDto dto);
    }
}
