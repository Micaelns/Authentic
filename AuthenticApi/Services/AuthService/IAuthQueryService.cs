using AuthenticApi.DTOs.Auth;
using AuthenticApi.DTOs.Users;
using System.Threading.Tasks;

namespace AuthenticApi.Services.AuthService
{
    public interface IAuthQueryService
    {
        Task<UserLogedDTO> Logon(LoginDTO loginDTO);
        Task Logout(string token);
    }
}
