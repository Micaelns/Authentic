using System.Collections.Generic;

namespace Authentic_Api.Models.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SoftwareId { get; set; }
        public SoftwareViewModel Software { get; set; }
        public List<PermissionViewModel> Permissions = new List<PermissionViewModel>(); 
    }
}