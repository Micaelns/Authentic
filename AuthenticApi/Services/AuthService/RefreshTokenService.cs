using Authentic_Api.Models.Entities;
using AuthenticApi.App_Data;
using AuthenticApi.DTOs.Users;
using AuthenticApi.Mappings;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace AuthenticApi.Services.AuthService
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly AuthenticContext _context;
        private readonly ITokenGenerator _tokenGenerator;

        public RefreshTokenService( AuthenticContext context, ITokenGenerator tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }
        
        public async Task<RefreshToken> Generate(UserLogedDTO user, string deviceId)
        {
            var currentUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            var newToken = await Generate(currentUser, deviceId);
            await SaveAsync(newToken);
            return newToken;
        }

        private async Task<RefreshToken> Generate(User user, string deviceId)
        {
            if (!int.TryParse(ConfigurationManager.AppSettings["JwtDaysToExpire"], out int daysToExpire))
            {
                daysToExpire = 1;
            }
            
            var tokenGenerated = _tokenGenerator.Generate();
            var token = RefreshToken.Create(user, tokenGenerated, deviceId, daysToExpire);
            
            return token;
        }

        private async Task SaveAsync(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> RefreshAsync(string refreshToken, string deviceId)
        {
            var token = await GetByHashAsync(refreshToken) ?? throw new Exception("RefreshToken não existe");

            if(!token.IsActive)
            {
                await RevokeAllUserTokensAsync(token.User.Id);
                throw new Exception("RefreshToken não é válido");
            }

            if (token.DeviceId != deviceId)
            {
                throw new Exception("RefreshToken inconsistente");
            }

            var newToken = await Generate(token.User, deviceId);
            token.Revoke(newToken.TokenHash);
            await SaveAsync(newToken);
            return newToken;
        }

        public async Task RevokeTokenAsync(string refreshToken, string deviceId)
        {
            var rToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.TokenHash == refreshToken) ?? throw new Exception("RefreshToken não existe");

            if (!rToken.IsActive)
            {
                throw new Exception("RefreshToken já está inativo");
            }

            if (rToken.DeviceId != deviceId)
            {
                throw new Exception("RefreshToken inconsistente");
            }

            rToken.Revoke();

            await _context.SaveChangesAsync();
        }

        private async Task<RefreshToken> GetByHashAsync(string hash)
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