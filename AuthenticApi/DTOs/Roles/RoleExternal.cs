using System.Collections.Generic;

namespace AuthenticApi.DTOs.Roles
{
    public class RoleExternal
    {
        public string Name { get; set; }
        public int SoftwareId { get; set; }
        public List<string> Permissions;
    }
}