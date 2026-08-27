using Authentic_Api.Models.Entities;
using AuthenticApi.App_Data;
using AuthenticApi.DTOs.Auth;
using AuthenticApi.DTOs.Roles;
using AuthenticApi.DTOs.Users;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticApi.Services.AuthService
{
    public class AuthQueryService : IAuthQueryService
    {
        private readonly AuthenticContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AuthQueryService(AuthenticContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher();
        }

        public async Task<UserLogedDTO> Logon(LoginDTO loginDTO)
        {
            var user = await  _context.Users
                .Where(item => item.DeletedAt == null && item.Email == loginDTO.Email)
                .Select(item => new UserLogingDTO
                {
                    Name = item.Name,
                    Email = item.Email,
                    IsBlocked = item.IsBlocked,
                    NickName = item.NickName,
                    PasswordHash = item.PasswordHash,
                    Roles = item.UserRoles.Select(itemRole => new RoleSimpleDTO
                    {
                        Name = itemRole.Role.Name,
                        SoftwareId = itemRole.Role.SoftwareId
                    })
                })
                .FirstOrDefaultAsync();

            if (user is null || IsAuthenticate(user, loginDTO)){
                throw new Exception("Email e/ou senha incorreto(s)");
            }

            if (user.IsBlocked == true)
            {
                throw new Exception("Usuário Bloqueado temporariamente");
            }

            if ( loginDTO.SoftwareId != 0 && !user.Roles.Any( item => item.SoftwareId == loginDTO.SoftwareId ))
            {
                throw new Exception("Usuário sem acesso a esse sistema. Fale com um Adminstrador.");
            }

            return new UserLogedDTO
            {
                Name = user.Name,
                NickName= user.NickName,
                Email = user.Email,
                Roles = user.Roles.Where(item => loginDTO.SoftwareId == 0 || loginDTO.SoftwareId == item.SoftwareId)
            };
        }

        private bool IsAuthenticate(UserLogingDTO user, LoginDTO loginDTO)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                user.PasswordHash,
                loginDTO.Password
            );

            return result == PasswordVerificationResult.Failed;
        }

        public Task Logout(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}