using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Interfaces;
using Carple.Domain.Enities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Carple.Persistance.Repository
{
   public class AuthRepo:IAuthRepo
    {
        private readonly IConfiguration  _configuration;
        public AuthRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var query = "SELECT * FROM Users WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }
    }
}
