using Authentic_Api.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticApi.Services.SoftwareService
{
    public interface ISoftwareQueryService
    {
        Task<SoftwareViewModel> GetById(int Id);
        Task<IEnumerable<SoftwareViewModel>> GetAllActives();
        Task<IEnumerable<SoftwareRolesCheckViewModel>> GetAllActivesWithRoles();
    }
}
