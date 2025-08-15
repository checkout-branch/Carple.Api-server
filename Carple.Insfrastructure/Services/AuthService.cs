using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Dto;
using Carple.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Dapper;
using Carple.Application.Interfaces.Repositories;
using Carple.Application.Interfaces.Services;
namespace Carple.Insfrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _repo;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepo repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }
  

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _repo.GetUserByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Email = user.Email,
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _repo.GetUserByEmailAsync(email);
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var apiKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Apikey = apiKey,
                RoleId = 1
            };

            await _repo.RegisterUserAsync(user);
            return apiKey;
        }


        public async Task<User> ValidateUserAsync(string email, string password)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("Default"));
            var query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email, Password = password });
        }


    }
}

