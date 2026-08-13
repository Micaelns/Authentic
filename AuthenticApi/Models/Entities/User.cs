using System;

namespace Authentic_Api.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public bool IsBlocked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

        public User() { }

        public User(string name, string nickName, string email, string phoneNumber, string PasswordHash, bool isBlocked)
        {
            Name = name;
            NickName = nickName;
            Email = email;
            PhoneNumber = phoneNumber;
            IsBlocked = isBlocked;
        }

        public static User CreateUser(string name, string nickName, string email, string phoneNumber, string passwordHash, bool isBlocked)
        {
            return new User(name, nickName, email, phoneNumber, passwordHash, isBlocked);
        }
    }
}