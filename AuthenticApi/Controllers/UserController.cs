using Authentic_Api.Models.ViewModels;
using AuthenticApi.Services.RoleService;
using AuthenticApi.Services.SoftwareService;
using AuthenticApi.Services.UserService;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AuthenticApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserQueryService _userQueryService;
        private readonly IUserCommandService _userCommandService;
        private readonly IUserAccessCommandService _userAccessCommandService;
        private readonly IRoleQueryService _roleQueryService;
        private readonly ISoftwareQueryService _softwareQueryService;
        public UserController(IUserQueryService userQueryService, IUserCommandService userCommandService, ISoftwareQueryService softwareQueryService, IRoleQueryService roleQueryService, IUserAccessCommandService userAccessCommandService)
        {
            _userQueryService = userQueryService;
            _userCommandService = userCommandService;
            _userAccessCommandService = userAccessCommandService;
            _softwareQueryService = softwareQueryService;
            _roleQueryService = roleQueryService;
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            var users = await _userQueryService.GetAllActives();

            return View(users);
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var user = await _userQueryService.GetById(id);

            if (user is null)
            {
                return HttpNotFound();
            }
            return View("Detalhes", user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            if (await _userQueryService.ExistsEmail(user.Email))
            {
                ModelState.AddModelError(
                    "Email",
                    "Este e-mail já está cadastrado."
                );
                return View(user);
            }

            if (await _userQueryService.ExistsEmail(user.NickName))
            {
                ModelState.AddModelError(
                    "NickName",
                    "Este NickName já está cadastrado."
                );
                return View(user);
            }

            try
            {
                await _userCommandService.Create(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/AccessSoftware/5
        public async Task<ActionResult> AccessSoftware(int id)
        {
            var user = await _userQueryService.GetAccessById(id);

            if (user is null)
            {
                return HttpNotFound();
            }

            var softwares = await _softwareQueryService.GetAllActivesWithRoles();

            foreach (var software in softwares)
            {
                foreach (var role in software.Roles)
                {
                    if ( user.Roles.Any(iten => iten.Id == role.Id) )
                    {
                        role.IsChecked = true;
                    }
                }
            }

            var userSoftware = new UserSoftwareViewModel
            {
                Softwares = softwares
                                .Where(x => x.Roles.Count > 0)
                                .ToList(),
                User = user,
            };

            return View(userSoftware);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AccessSoftware(int id, UserSoftwareViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _userAccessCommandService.Update(model);

            return RedirectToAction("Index");
        }
        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var user = await _userQueryService.GetById(id);

            if (user is null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            if (await _userQueryService.ExistsEmail(user.Email, user.Id))
            {
                ModelState.AddModelError(
                    "Email",
                    "Este e-mail já está cadastrado."
                );
                return View(user);
            }

            if (await _userQueryService.ExistsEmail(user.NickName, user.Id))
            {
                ModelState.AddModelError(
                    "NickName",
                    "Este NickName já está cadastrado."
                );
                return View(user);
            }

            var userDAO = await _userQueryService.GetById(id);
            if (userDAO is null)
            {
                return HttpNotFound();
            }

            try
            {
                await _userCommandService.Update(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var userFound = await _userQueryService.GetById(id);

            if (userFound is null)
            {
                return HttpNotFound();
            }
            return View(userFound);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, UserViewModel user)
        {
            try
            {
                await _userCommandService.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
