using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticApi.Services.RoleService
{
    public class RoleQueryService : IRoleQueryService
    {
        private readonly AuthenticContext _context;
        public RoleQueryService(AuthenticContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleViewModel>> GetActiveRolesBySoftwareId(int softwareId)
        {
            return await _context.Roles
                .AsNoTracking()
                .Where(x => x.DeletedAt == null && x.SoftwareId == softwareId)
                .Select(x => new RoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
               .OrderBy(x => x.Name)
               .ToListAsync();

        }

        public async Task<RoleViewModel> GetById(int id)
        {
            return await _context.Roles
                .AsNoTracking()
                .Where(x => x.DeletedAt == null && x.Id == id)
                .Select(x => new RoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Software = new SoftwareViewModel { Id = x.SoftwareId, Name = x.Software.Name, Description = x.Software.Description },
                    Permissions = x.RolePermissions.Select(rperm => new PermissionViewModel
                    {
                        Id = rperm.PermissionId,
                        Code = rperm.Permission.Code,
                        Description = rperm.Permission.Description,
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}