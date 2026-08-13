using Authentic_Api.Models.ViewModels;
using System.Threading.Tasks;

namespace AuthenticApi.Services.SoftwareService
{
    public interface ISoftwareCommandService
    {
        Task Create(SoftwareViewModel user);
        Task Update(SoftwareViewModel user);
        Task Delete(int id);
    }
}
