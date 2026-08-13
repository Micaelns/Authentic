using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticApi.Services.UserService
{
    public class UserQueryService : IUserQueryService
    {
        private readonly AuthenticContext _context;

        public UserQueryService(AuthenticContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsEmail(string Email, int NotId = 0)
        {
            return await _context.Users.AnyAsync(x => x.Email == Email && (NotId == 0 || x.Id != NotId));
        }

        public async Task<bool> ExistsNickName(string NickName, int NotId = 0)
        {
            return await _context.Users.AnyAsync(x => x.NickName == NickName && (NotId == 0 || x.Id != NotId));
        }

        public async Task<IEnumerable<UserViewModel>> GetAllActives()
        {
            return await _context.Users
                .AsNoTracking()
                .Where(x => x.DeletedAt == null)
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    NickName = x.NickName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    IsBlocked = x.IsBlocked
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<UserViewModel> GetById(int Id)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(x => x.DeletedAt == null && x.Id == Id)
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    NickName = x.NickName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    IsBlocked = x.IsBlocked
                })
                .FirstOrDefaultAsync();
        }
    }
}