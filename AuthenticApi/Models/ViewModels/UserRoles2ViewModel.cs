using Authentic_Api.Models.ViewModels;
using System.Collections.Generic;

namespace AuthenticApi.Models.ViewModels
{
    public class UserRoles2ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsBlocked { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}