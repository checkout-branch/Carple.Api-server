using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Dto;

namespace Carple.Application.Interfaces
{
   public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
    }
}
