using System;

namespace Authentic_Api.Models.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; } = 0;
        public User User { get; set; } = new User();
        public string TokenHash { get; private set; }
        public string DeviceId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; private set; } = DateTime.UtcNow.AddDays(1);
        public DateTime? RevokedAt { get; private set; }
        public string ReplacedByTokenHash { get; private set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive => RevokedAt == null && !IsExpired;

        public void Revoke(string replacedBy = null)
        {
            RevokedAt = DateTime.UtcNow;
            ReplacedByTokenHash = replacedBy;
        }

        public static RefreshToken Create(User user, string tokenHash, string deviceId, int daysToExpire)
        {
            return new RefreshToken()
            {
                User = user,
                TokenHash = tokenHash,
                DeviceId = deviceId,
                ExpiresAt = DateTime.UtcNow.AddDays(daysToExpire)
            };
        }
    }
}