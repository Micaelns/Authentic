using Authentic_Api.Models.Entities;
using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Entities = Authentic_Api.Models.Entities;

namespace AuthenticApi.Services.RoleService
{
    public class RoleCommandService : IRoleCommandService
    {
        private readonly AuthenticContext _context;

        public RoleCommandService(AuthenticContext context)
        {
            _context = context;
        }

        private async Task<Entities.Role> GetById(int id)
        {
            return await _context.Roles
                .Include(r => r.RolePermissions)
                .Where(x => x.DeletedAt == null && x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Create(int softwareId, RoleCreateViewModel roleView)
        {
            var roleDao = new Role { Name = roleView.Role.Name, SoftwareId = softwareId };

            var rolePermissions = roleView.Permissions
                                .Where(item => item.IsChecked)
                                .Select(item => new RolePermission { PermissionId = item.Id, Role = roleDao });

            _context.Roles.Add(roleDao);
            _context.RolePermissions.AddRange(rolePermissions);

            await _context.SaveChangesAsync();
        }

        public async Task Update(int softwareId, RoleCreateViewModel roleView)
        {
            var roleDao = await GetById(roleView.Role.Id) ?? throw new KeyNotFoundException("Role não existe.");

            var rolePermissionsToRemove = roleDao.RolePermissions
                                            .Where(itemDB => roleView.Permissions.Any(itenView => !itenView.IsChecked && itemDB.PermissionId == itenView.Id))
                                            .ToList();

            var rolePermissionsToAdd = roleView.Permissions
                                .Where(itemView => itemView.IsChecked && !roleDao.RolePermissions.Any(itemDB => itemDB.PermissionId == itemView.Id))
                                .Select(item => new RolePermission { PermissionId = item.Id, RoleId = roleDao.Id });

            roleDao.Name = roleView.Role.Name;
            _context.RolePermissions.RemoveRange(rolePermissionsToRemove);
            _context.RolePermissions.AddRange(rolePermissionsToAdd);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var roleDao = await GetById(id) ?? throw new KeyNotFoundException("Role não existe.");
            roleDao.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}