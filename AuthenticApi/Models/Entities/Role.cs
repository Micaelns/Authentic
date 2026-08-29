using System;
using System.Collections.Generic;

namespace Authentic_Api.Models.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SoftwareId { get; set; }
        public virtual Software Software { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
    }
}