using System;

namespace Authentic_Api.Models.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}