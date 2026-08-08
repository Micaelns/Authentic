using Authentic_Api.Models.Entities;
using System.Data.Entity;

namespace AuthenticApi.App_Data
{
    public class AuthenticContext : DbContext
    {
        public AuthenticContext()
           : base("AuthenticConnection")
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}