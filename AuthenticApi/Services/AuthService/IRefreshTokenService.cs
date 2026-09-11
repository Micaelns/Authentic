using Authentic_Api.Models.Entities;
using AuthenticApi.DTOs.Users;
using System.Threading.Tasks;

namespace AuthenticApi.Services.AuthService
{

    public interface IRefreshTokenService
    {
        Task<string> Generate(UserLogedDTO user, string deviceId);
        Task<(User, string)> RotateAsync(string refreshToken, string deviceId);
        Task RevokeTokenAsync(string refreshToken, string deviceId);
    }
}
