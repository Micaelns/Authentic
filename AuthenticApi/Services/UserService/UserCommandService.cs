using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Entities = Authentic_Api.Models.Entities;

namespace AuthenticApi.Services.UserService
{
    public class UserCommandService : IUserCommandService
    {
        private readonly AuthenticContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserCommandService(AuthenticContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher();
        }

        private async Task<Entities.User> GetById(int id)
        {
            return await _context.Users
                .Where(x => x.DeletedAt == null && x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Create(UserViewModel user)
        {
            var password = _passwordHasher.HashPassword(user.Password);
            var userDAO = Entities.User.CreateUser(user.Name, user.NickName, user.Email, user.PhoneNumber, password, user.IsBlocked);
            _context.Users.Add(userDAO);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserViewModel user)
        {
            var userDao = await GetById(user.Id) ?? throw new KeyNotFoundException("Usuário não existe.");

            userDao.Name = user.Name;
            userDao.NickName = user.NickName;
            userDao.Email = user.Email;
            userDao.NickName = user.NickName;
            userDao.PhoneNumber = user.PhoneNumber;
            userDao.IsBlocked = user.IsBlocked;
            if (!string.IsNullOrEmpty(user.Password))
            {
                userDao.PasswordHash = _passwordHasher.HashPassword(user.Password);
            }
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var userDao = await GetById(id) ?? throw new KeyNotFoundException("Usuário não existe.");
            userDao.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}