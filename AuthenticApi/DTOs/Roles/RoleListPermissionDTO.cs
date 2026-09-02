using System.Collections.Generic;

namespace AuthenticApi.DTOs.Roles
{
    public class RoleListPermissionDTO
    {
        public string Name { get; set; }
        public List<string> Permissions;
    }
}