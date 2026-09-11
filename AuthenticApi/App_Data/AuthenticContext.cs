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
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                    .HasMany(x => x.RolePermissions)
                    .WithRequired(x => x.Role)
                    .HasForeignKey(x => x.RoleId)
                    .WillCascadeOnDelete(true);

            modelBuilder.Entity<Software>()
                    .HasMany(x => x.Roles)
                    .WithRequired(x => x.Software)
                    .HasForeignKey(x => x.SoftwareId)
                    .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                    .HasMany(x => x.UserRoles)
                    .WithRequired(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .WillCascadeOnDelete(true);
        }
    }
}