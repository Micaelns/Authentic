using Authentic_Api.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticApi.Services.UserService
{
    public interface IUserQueryService
    {
        Task<UserViewModel> GetById(int Id);
        Task<bool> ExistsNickName(string NickName, int NotId = 0);
        Task<bool> ExistsEmail(string Email, int NotId = 0);
        Task<IEnumerable<UserViewModel>> GetAllActives();
    }
}