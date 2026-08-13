using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
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

        public UserCommandService(AuthenticContext context)
        {
            _context = context;
        }

        private async Task<Entities.User> GetById(int id)
        {
            return await _context.Users
                .Where(x => x.DeletedAt == null && x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Create(UserViewModel user)
        {
            var userDAO = Entities.User.CreateUser(user.Name, user.NickName, user.Email, user.PhoneNumber, user.PasswordHash, user.IsBlocked);
            _context.Users.Add(userDAO);
            _context.SaveChanges();
        }

        public async Task Update(UserViewModel user)
        {
            var userDao = await GetById(user.Id) ?? throw new KeyNotFoundException("Usuário não existe.");

            userDao.Name= user.Name;
            userDao.NickName = user.NickName;
            userDao.Email = user.Email;
            userDao.NickName = userDao.NickName;
            userDao.PhoneNumber = userDao.PhoneNumber;
            if (!string.IsNullOrEmpty(userDao.PasswordHash))
            {
                userDao.PasswordHash = userDao.PasswordHash;
            }
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var userDao = await GetById(id) ?? throw new KeyNotFoundException("Usuário não existe.");
            _context.Users.Remove(userDao);
            _context.SaveChanges();

        }
    }
}