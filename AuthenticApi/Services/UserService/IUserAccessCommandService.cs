using Authentic_Api.Models.ViewModels;
using System.Threading.Tasks;

namespace AuthenticApi.Services.UserService
{
    public interface IUserAccessCommandService
    {
        Task Update(UserSoftwareViewModel userAccess);
    }
}
