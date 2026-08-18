using Authentic_Api.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticApi.Services.PermissionService
{
    public interface IPermissionQueryService
    {
        Task<IEnumerable<PermissionViewModel>> GetActives();
    }
}