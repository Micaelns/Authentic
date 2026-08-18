using Authentic_Api.Models.ViewModels;
using System.Threading.Tasks;

namespace AuthenticApi.Services.RoleService
{
    public interface IRoleCommandService
    {
        Task Create(int softwareId, RoleCreateViewModel roleView);
        Task Update(int softwareId, RoleCreateViewModel roleView);
        Task Delete(int id);
    }
}
