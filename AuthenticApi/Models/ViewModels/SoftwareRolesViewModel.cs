using System.Collections.Generic;

namespace Authentic_Api.Models.ViewModels
{
    public class SoftwareRolesViewModel
    {
        public SoftwareViewModel Software { get; set; }
        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}