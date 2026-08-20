using Authentic_Api.Models.ViewModels;
using AuthenticApi.Services.PermissionService;
using AuthenticApi.Services.RoleService;
using AuthenticApi.Services.SoftwareService;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AuthenticApi.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleQueryService _roleQueryService;
        private readonly IRoleCommandService _roleCommandService;
        private readonly ISoftwareQueryService _softwareQueryService;
        private readonly IPermissionQueryService _permissionQueryService;
        public RoleController(IRoleQueryService roleQueryService, IRoleCommandService roleCommandService, ISoftwareQueryService softwareQueryService, IPermissionQueryService permissionQueryService)
        {
            _roleQueryService = roleQueryService;
            _roleCommandService = roleCommandService;
            _softwareQueryService = softwareQueryService;
            _permissionQueryService = permissionQueryService;
        }

        // GET: software/{softwareId}/Role
        public async Task<ActionResult> Index(int softwareId)
        {
            var viewModel = new SoftwareRolesViewModel
            {
                Software = await _softwareQueryService.GetById(softwareId),
                Roles = (List<RoleViewModel>) await _roleQueryService.GetActiveRolesBySoftwareId(softwareId)
            };
            return View(viewModel);
        }

        // GET: software/{softwareId}/Role/{id}/Details
        public async Task<ActionResult> Details(int softwareId, int id)
        {
            var role = await _roleQueryService.GetById(id);

            if (role is null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public async Task<ActionResult> Create(int softwareId)
        {
            var permissions = await _permissionQueryService.GetActives();
            var model = new RoleCreateViewModel
            {
                Role = new RoleViewModel { SoftwareId = softwareId },
                Permissions = permissions.Select(x => new PermissionCheckViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsChecked = false
                }).ToList()
            };
            return View(model);
        }

        // GET: software/{softwareId}/Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int softwareId, RoleCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _roleCommandService.Create(softwareId, model);

                return RedirectToAction("Index", softwareId);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: software/{softwareId}/Role/Edit/{id}
        public async Task<ActionResult> Edit(int softwareId, int id)
        {
            var role = await _roleQueryService.GetById(id);

            if (role is null)
            {
                return HttpNotFound();
            }
            var permissions = await _permissionQueryService.GetActives();
            var model = new RoleCreateViewModel
            {
                Role = role,
                Permissions = permissions.Select(x => new PermissionCheckViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Description = x.Description,
                    IsChecked = role.Permissions.Any(iten => iten.Id == x.Id)
                }).ToList()
            };

            return View(model);
        }

        // POST: software/{softwareId}/Roles/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int softwareId, RoleCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _roleCommandService.Update(softwareId, model);

                return RedirectToAction("Index", softwareId);
            }
            catch
            {
                return View(model);
            }
        }

        // POST: software/{softwareId}/Roles/Delete/{id}
        public async Task<ActionResult> Delete(int softwareId, int id)
        {
            var userFound = await _roleQueryService.GetById(id);

            if (userFound is null)
            {
                return HttpNotFound();
            }
            return View(userFound);
        }

        // DELETE: software/{softwareId}/Roles/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int softwareId, int id)
        {
            try
            {
                await _roleCommandService.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
