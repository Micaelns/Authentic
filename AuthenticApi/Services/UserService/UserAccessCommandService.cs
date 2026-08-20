using Authentic_Api.Models.Entities;
using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticApi.Services.UserService
{
    public class UserAccessCommandService : IUserAccessCommandService
    {
        private readonly AuthenticContext _context;

        public UserAccessCommandService(AuthenticContext context)
        {
            _context = context;
        }

        public async Task Update(UserSoftwareViewModel userAccess)
        {
            var userRoleDao = await _context.UserRoles
                .Where(x => x.UserId == userAccess.User.Id)
                .ToListAsync();

            foreach (var software in userAccess.Softwares)
            {
                var rolesRemove = userRoleDao
                                    .Where(itemDB => software.Roles.Any(element => element.Id == itemDB.RoleId && !element.IsChecked))
                                    .ToList();
                _context.UserRoles.RemoveRange(rolesRemove);
            }

            foreach (var software in userAccess.Softwares)
            {
                var rolesAdd = software.Roles
                                    .Where(element => !userRoleDao.Any(itemDB => element.Id == itemDB.RoleId) && element.IsChecked)
                                    .Select(element => new UserRole
                                    {
                                        RoleId = element.Id,
                                        UserId = userAccess.User.Id
                                    })
                                    .ToList();
                _context.UserRoles.AddRange(rolesAdd);
            }

            await _context.SaveChangesAsync();
        }
    }
}