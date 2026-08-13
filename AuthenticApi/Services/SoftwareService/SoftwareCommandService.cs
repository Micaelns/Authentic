using Authentic_Api.Models.Entities;
using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Entities = Authentic_Api.Models.Entities;

namespace AuthenticApi.Services.SoftwareService
{
    public class SoftwareCommandService : ISoftwareCommandService
    {
        private readonly AuthenticContext _context;

        public SoftwareCommandService(AuthenticContext context)
        {
            _context = context;
        }
        private async Task<Entities.Software> GetById(int id)
        {
            return await _context.Softwares
                .Where(x => x.DeletedAt == null && x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Create(SoftwareViewModel software)
        {
            var softwareDao = new Software { Name = software.Name, Description = software.Description };
            _context.Softwares.Add(softwareDao);
            await _context.SaveChangesAsync();
        }

        public async Task Update(SoftwareViewModel software)
        {
            var softwareDao = await GetById(software.Id) ?? throw new KeyNotFoundException("Software não existe.");

            softwareDao.Name = software.Name;
            softwareDao.Description = software.Description;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var softwareDao = await GetById(id) ?? throw new KeyNotFoundException("Software não existe.");
            _context.Softwares.Remove(softwareDao);
            _context.SaveChanges();
        }
    }
}