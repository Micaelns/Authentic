using Authentic_Api.Models.Entities;
using AuthenticApi.App_Data;
using AuthenticApi.DTOs.Users;
using AuthenticApi.Exceptions;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticApi.Services.AuthService
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly AuthenticContext _context;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly ITokenHasher _tokenHasher;

        public RefreshTokenService(AuthenticContext context, IRefreshTokenGenerator refreshTokenGenerator, ITokenHasher tokenHasher)
        {
            _context = context;
            _refreshTokenGenerator = refreshTokenGenerator;
            _tokenHasher = tokenHasher;
        }

        public async Task<string> Generate(UserLogedDTO user, string deviceId)
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            return await Generate(currentUser, deviceId, null);
        }

        private async Task<string> Generate(User user, string deviceId, RefreshToken refreshTokenToRevoke)
        {
            if (!int.TryParse(ConfigurationManager.AppSettings["JwtDaysToExpire"], out int daysToExpire))
            {
                daysToExpire = 1;
            }

            var refreshTokenGenerated = _refreshTokenGenerator.Generate();
            var refreshTokenHash = _tokenHasher.Hash(refreshTokenGenerated);
            var refreshToken = RefreshToken.Create(user, refreshTokenHash, deviceId, daysToExpire);

            refreshTokenToRevoke?.Revoke(refreshTokenHash);
            await SaveAsync(refreshToken);

            return refreshTokenGenerated;
        }

        private async Task SaveAsync(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task<(User,string)> RotateAsync(string refreshToken, string deviceId)
        {
            var refreshTokenHash = _tokenHasher.Hash(refreshToken);
            var refreshTokenObj = await GetRefreshTokenByHashAsync(refreshTokenHash) ?? throw new NotFoundRefreshTokenException();

            if (!refreshTokenObj.IsActive)
            {
                await RevokeAllUserTokensAsync(refreshTokenObj.User.Id);
                throw new DisabledRefreshTokenException();
            }

            if (refreshTokenObj.DeviceId != deviceId)
            {
                throw new IncisiveRefreshTokenException();
            }

            var newRefreshToken = await Generate(refreshTokenObj.User, deviceId, refreshTokenObj);
            return (refreshTokenObj.User, newRefreshToken);
        }

        public async Task RevokeTokenAsync(string refreshToken, string deviceId)
        {
            var refreshTokenHash = _tokenHasher.Hash(refreshToken);
            var rToken = await _context.RefreshTokens
                .Include(c => c.User)
                .FirstOrDefaultAsync(x => x.TokenHash == refreshTokenHash) ?? throw new NotFoundRefreshTokenException();

            if (!rToken.IsActive)
            {
                await RevokeAllUserTokensAsync(rToken.User.Id);
                throw new DisabledRefreshTokenException();
            }

            if (rToken.DeviceId != deviceId)
            {
                throw new IncisiveRefreshTokenException();
            }

            rToken.Revoke();

            await _context.SaveChangesAsync();
        }

        private async Task<RefreshToken> GetRefreshTokenByHashAsync(string hash)
        {
            return await _context.RefreshTokens
                .Include(c => c.User)
                .FirstOrDefaultAsync(x => x.TokenHash == hash);
        }

        private async Task RevokeAllUserTokensAsync(int userId)
        {
            var tokens = await _context.RefreshTokens
                .Where(x => x.User.Id == userId && x.RevokedAt == null)
                .ToListAsync();

            foreach (var token in tokens)
                token.Revoke();

            await _context.SaveChangesAsync();
        }

    }
}