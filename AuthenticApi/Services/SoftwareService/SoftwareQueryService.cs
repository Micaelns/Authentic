using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticApi.Services.SoftwareService
{
    public class SoftwareQueryService : ISoftwareQueryService
    {
        private readonly AuthenticContext _context;

        public SoftwareQueryService(AuthenticContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SoftwareViewModel>> GetAllActives()
        {
            return await _context.Softwares
                .AsNoTracking()
                .Where(sof => sof.DeletedAt == null)
                .Select(sof => new SoftwareViewModel
                {
                    Id = sof.Id,
                    Name = sof.Name,
                    Description = sof.Description
                })
               .OrderBy(sof => sof.Name)
               .ToListAsync();
        }

        public async Task<IEnumerable<SoftwareRolesCheckViewModel>> GetAllActivesWithRoles()
        {
            return await _context.Softwares
                .AsNoTracking()
                .Where(sof => sof.DeletedAt == null)
                .OrderBy(sof => sof.Name)
                .Select(sof => new SoftwareRolesCheckViewModel
                {
                    Software = new SoftwareViewModel { Id = sof.Id, Name = sof.Name, Description = sof.Description },
                    Roles = sof.Roles.Select(element => new RoleCheckViewModel
                    {
                        Id = element.Id,
                        Name = element.Name
                    }).ToList()
                })
               .ToListAsync();
        }

        public async Task<SoftwareViewModel> GetById(int Id)
        {
            return await _context.Softwares
                .AsNoTracking()
                .Where(x => x.DeletedAt == null && x.Id == Id)
                .Select(x => new SoftwareViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .FirstOrDefaultAsync();
        }
    }
}