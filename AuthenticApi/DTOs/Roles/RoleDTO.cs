using Authentic_Api.Models.ViewModels;
using AuthenticApi.DTOs.Softwares;
using System.Collections.Generic;

namespace AuthenticApi.DTOs.Roles
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SoftwareDTO Software { get; set; }
        public IEnumerable<PermissionViewModel> Permissions;
    }
}