using System;
using System.Security.Cryptography;

namespace AuthenticApi.Services.AuthService
{
    public class TokenGenerator : ITokenGenerator
    {
        public string Generate()
        {
            byte[] randomBytes = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}