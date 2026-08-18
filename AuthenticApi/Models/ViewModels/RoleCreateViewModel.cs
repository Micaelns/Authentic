using System.Collections.Generic;

namespace Authentic_Api.Models.ViewModels
{
    public class RoleCreateViewModel
    {
        public RoleViewModel Role {  get; set; }
        public IList<PermissionCheckViewModel> Permissions { get; set; } = new List<PermissionCheckViewModel>();
    }
}