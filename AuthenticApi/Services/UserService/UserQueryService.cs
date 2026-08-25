using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
using AuthenticApi.DTOs.Roles;
using AuthenticApi.DTOs.Softwares;
using AuthenticApi.DTOs.Users;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticApi.Services.UserService
{
    public class UserQueryService : IUserQueryService
    {
        private readonly AuthenticContext _context;

        public UserQueryService(AuthenticContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsEmail(string Email, int NotId = 0)
        {
            return await _context.Users.AnyAsync(x => x.Email == Email && (NotId == 0 || x.Id != NotId));
        }

        public async Task<bool> ExistsNickName(string NickName, int NotId = 0)
        {
            return await _context.Users.AnyAsync(x => x.NickName == NickName && (NotId == 0 || x.Id != NotId));
        }

        public async Task<IEnumerable<UserViewModel>> GetAllActives()
        {
            return await _context.Users
                .AsNoTracking()
                .Where(x => x.DeletedAt == null)
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    NickName = x.NickName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    IsBlocked = x.IsBlocked
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<UserViewModel> GetById(int Id)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(x => x.DeletedAt == null && x.Id == Id)
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    NickName = x.NickName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    IsBlocked = x.IsBlocked
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UserDTO> GetAccessById(int Id)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(x => x.DeletedAt == null && x.Id == Id)
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    NickName = x.NickName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    IsBlocked = x.IsBlocked,
                    Roles = x.UserRoles.Select(iten => new RoleDTO
                    {
                        Id = iten.Role.Id,
                        Name = iten.Role.Name,
                        Software = new SoftwareDTO
                        {
                            Id = iten.Role.Software.Id,
                            Name = iten.Role.Software.Name,
                            Description = iten.Role.Software.Description
                        },
                        Permissions = iten.Role.RolePermissions.Select(itemRole => new PermissionViewModel
                        {
                            Id = itemRole.Permission.Id,
                            Code = itemRole.Permission.Code,
                            Description = itemRole.Permission.Description
                        })
                    })
                })
                .FirstOrDefaultAsync();
        }
    }
}