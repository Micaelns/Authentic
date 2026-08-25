using AuthenticApi.DTOs.Users;

namespace AuthenticApi.Services.AuthService
{
    public interface IJwtTokenService
    {
        string GenerateToken(UserLogedDTO user);
    }
}
