using Authentic_Api.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticApi.Services.RoleService
{
    public interface IRoleQueryService
    {
        Task<RoleViewModel> GetById(int id);
        Task<IEnumerable<RoleViewModel>> GetActiveRolesBySoftwareId(int softwareId);
    }
}
