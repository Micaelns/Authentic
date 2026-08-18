using Authentic_Api.Models.Entities;

namespace Authentic_Api.Models.ViewModels
{
    public class RolePermissionViewModel
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}