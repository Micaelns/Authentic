using System.Collections.Generic;

namespace Authentic_Api.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsBlocked { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}