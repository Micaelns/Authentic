using Authentic_Api.Models.Entities;
using AuthenticApi.DTOs.Users;
using System.Threading.Tasks;

namespace AuthenticApi.Services.AuthService
{

    public interface IRefreshTokenService
    {
        Task<RefreshToken> Generate(UserLogedDTO user, string deviceId);
        Task<RefreshToken> RefreshAsync(string refreshToken, string deviceId);
        Task RevokeTokenAsync(string refreshToken, string deviceId);
    }
}
