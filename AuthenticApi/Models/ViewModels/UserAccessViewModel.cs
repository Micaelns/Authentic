using Authentic_Api.Models.ViewModels;
using System.Collections.Generic;

namespace AuthenticApi.Models.ViewModels
{
    public class UserAccessViewModel
    {
        public IEnumerable<RoleViewModel> Roles { get; set; }

        public UserRolesViewModel UserRole { get; set; }
    }
}