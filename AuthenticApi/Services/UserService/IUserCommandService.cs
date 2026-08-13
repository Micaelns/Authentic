using Authentic_Api.Models.ViewModels;
using System.Threading.Tasks;
using Entities = Authentic_Api.Models.Entities;

namespace AuthenticApi.Services.UserService
{
    public interface IUserCommandService
    {
        Task Create(UserViewModel user);
        Task Update(UserViewModel user);
        Task Delete(int id);
    }
}