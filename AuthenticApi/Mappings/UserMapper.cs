using Authentic_Api.Models.ViewModels;
using AuthenticApi.DTOs.Users;
using System.Linq;

namespace AuthenticApi.Mappings
{
    public static class UserMapper
    {
        public static UserRolesViewModel ToUserRolesViewModel(UserDTO dto)
        {
            return new UserRolesViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                NickName = dto.NickName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                IsBlocked = dto.IsBlocked,
                Roles = dto.Roles.Select(iten => new RoleViewModel
                {
                    Id = iten.Id,
                    Name = iten.Name,
                    SoftwareId = iten.Software.Id,
                    Software = new SoftwareViewModel
                    {
                        Id = iten.Software.Id,
                        Name = iten.Software.Name,
                        Description = iten.Software.Description
                    },
                    Permissions = iten.Permissions.Select(itemPermission => new PermissionViewModel
                    {
                        Id = itemPermission.Id,
                        Code = itemPermission.Code,
                        Description = itemPermission.Description
                    }).ToList()
                })
            };
        }
    }
}