using AuthenticApi.App_Data;
using AuthenticApi.DTOs.Auth;
using AuthenticApi.DTOs.Roles;
using AuthenticApi.DTOs.Users;
using AuthenticApi.Exceptions;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
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
                    Id = item.Id,
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

            if (user is null || IsPasswordInvalid(user, loginDTO)){
                throw new InvalidCredentialsException();
            }

            if (user.IsBlocked == true)
            {
                throw new UserBlockedException();
            }

            if ( loginDTO.SoftwareId != 0 && !user.Roles.Any( item => item.SoftwareId == loginDTO.SoftwareId ))
            {
                throw new ForbiddenSoftwareAccessException();
            }

            return new UserLogedDTO
            {
                Id = user.Id,
                Name = user.Name,
                NickName= user.NickName,
                Email = user.Email,
            };
        }

        private bool IsPasswordInvalid(UserLogingDTO user, LoginDTO loginDTO)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                user.PasswordHash,
                loginDTO.Password
            );

            return result == PasswordVerificationResult.Failed;
        }
    }
}