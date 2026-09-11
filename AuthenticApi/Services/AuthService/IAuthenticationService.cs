using AuthenticApi.DTOs.Auth;
using System.Threading.Tasks;

namespace AuthenticApi.Services.AuthService
{
    public interface IAuthenticationService
    {
        Task<TokenDTO> LoginAsync(LoginDTO user);
        Task<TokenDTO> RefreshAsync(string refreshToken, string deviceId);
        Task LogoutAsync(string refreshToken, string deviceId);
    }
}
