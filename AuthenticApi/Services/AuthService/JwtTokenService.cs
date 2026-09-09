using AuthenticApi.DTOs.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticApi.Services.AuthService
{
    public class JwtTokenService : IJwtTokenService
    {
        public string GenerateToken(UserLogedDTO user)
        {
            var jwtSecret = ConfigurationManager.AppSettings["JwtSecret"];
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.GivenName, user.NickName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials,
                issuer: ConfigurationManager.AppSettings["JwtIssuer"]
                );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}