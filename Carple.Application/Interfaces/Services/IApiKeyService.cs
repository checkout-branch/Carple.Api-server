using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carple.Application.Interfaces.Services
{
    public interface IApiKeyService
    {
        Task<bool> ValidateKeyAsync(string apiKey);
    }
}
