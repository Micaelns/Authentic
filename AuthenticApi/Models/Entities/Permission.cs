using System;

namespace Authentic_Api.Models.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
    }
}