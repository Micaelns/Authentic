using AuthenticApi.DTOs.Roles;
using System.Collections.Generic;

namespace AuthenticApi.DTOs.Users
{
    public class UserLogingDTO
    {
        public string Name { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public bool IsBlocked { get; set; }
        public IEnumerable<RoleSimpleDTO> Roles { get; set; }
    }
}