using System.Collections.Generic;

namespace Authentic_Api.Models.ViewModels
{
    public class SoftwareRolesCheckViewModel
    {
        public SoftwareViewModel Software { get; set; }
        public List<RoleCheckViewModel> Roles { get; set; }

    }
}