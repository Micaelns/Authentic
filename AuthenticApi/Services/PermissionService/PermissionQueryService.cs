using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticApi.Services.PermissionService
{
    public class PermissionQueryService : IPermissionQueryService
    {
        private AuthenticContext _context;

        public PermissionQueryService(AuthenticContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermissionViewModel>> GetActives()
        {
            return await _context.Permissions
                .AsNoTracking()
                .Where(x => x.DeletedAt == null)
                .Select(x => new PermissionViewModel()
                {
                   Id = x.Id,
                   Code = x.Code,
                   Description = x.Description
                })
                .OrderBy(x => x.Code)
               .ToListAsync();
        }
    }
}