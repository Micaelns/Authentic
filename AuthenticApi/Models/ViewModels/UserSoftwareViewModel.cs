using AuthenticApi.Models.ViewModels;
using System.Collections.Generic;

namespace Authentic_Api.Models.ViewModels
{
    public class UserSoftwareViewModel
    {
        public List<SoftwareRolesCheckViewModel> Softwares { get; set; }
        public UserRolesViewModel User { get; set; }
    }
}