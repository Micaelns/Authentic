using Authentic_Api.Models.Entities;
using AuthenticApi.DTOs.Auth;
using AuthenticApi.Mappings;
using System.Threading.Tasks;

namespace AuthenticApi.Services.AuthService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthQueryService _authQueryService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthenticationService(IAuthQueryService authQueryService, IJwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService)
        {
            _authQueryService = authQueryService;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<TokenDTO> LoginAsync(LoginDTO user)
        {
            var userloged = await _authQueryService.Logon(user);
            var accessToken = _jwtTokenService.GenerateToken(userloged);
            var refreshToken = await _refreshTokenService.Generate(userloged, user.DeviceId);

            return new TokenDTO { Token = accessToken, RefreshToken = refreshToken };
        }

        public async Task LogoutAsync(string refreshToken, string deviceId)
        {
           await _refreshTokenService.RevokeTokenAsync(refreshToken, deviceId);
        }

        public async Task<TokenDTO> RefreshAsync(string refreshToken, string deviceId)
        {
            (User user, string newRefreshToken) = await _refreshTokenService.RotateAsync(refreshToken, deviceId);

            var token = _jwtTokenService.GenerateToken(UserMapper.ToUserLogedDTO(user));

            return new TokenDTO { Token = token,  RefreshToken = newRefreshToken };
        }
    }
}