using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Interfaces.Repositories;
using Carple.Domain.Enities;
using Carple.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Carple.Persistance.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private readonly IConfiguration _configuration;
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

        public async Task<int> RegisterUserAsync(User user)
        {
            //var query = "INSERT INTO Users (FullName, Email, PasswordHash, ApiKey, RoleId) VALUES (@FullName, @Email, @PasswordHash, @ApiKey, @RoleId)";
            var query = "INSERT INTO Users (FullName, Email, PasswordHash, RoleId)  VALUES(@FullName, @Email, @PasswordHash, @RoleId)";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return await connection.ExecuteAsync(query, user);
        }

    }
}
