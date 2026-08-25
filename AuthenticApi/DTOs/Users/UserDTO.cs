using AuthenticApi.DTOs.Roles;
using System.Collections.Generic;

namespace AuthenticApi.DTOs.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsBlocked { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
    }
}