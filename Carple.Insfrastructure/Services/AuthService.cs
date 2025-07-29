using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Dto;
using Carple.Application.Interfaces;
using Carple.Domain.Enities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace Carple.Insfrastructure.Services
{
   public class AuthService:IAuthService
    {
        private readonly IAuthRepo _repo;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepo repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }
        //public async Task<LoginResponseDto>LoginAsync(LoginDto dto)
        //{
        //    var user = await _repo.GetUserByEmailAsync(dto.Email);
        //    if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        //        return null;
        //    var token = GenerateJwtToken(user);
        //    return new LoginResponseDto
        //    {
        //        Email = user.Email,
        //        Token = token
        //    };
        //}
        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _repo.GetUserByEmailAsync(dto.Email);

            // ✅ Compare directly (Plain-text comparison — for testing only)
            if (user == null || user.PasswordHash != dto.Password)
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
    }
}

