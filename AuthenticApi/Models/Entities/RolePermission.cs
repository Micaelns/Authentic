using System;

namespace Authentic_Api.Models.Entities
{
    public class RolePermission
    {
        public int Id { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}