using Authentic_Api.Models.Entities;
using Authentic_Api.Models.ViewModels;
using AuthenticApi.App_Data;
using System.Linq;
using System.Web.Mvc;

namespace AuthenticApi.Controllers
{
    public class UserController : Controller
    {
        private readonly AuthenticContext _context;
        public UserController()
        {
            _context = new AuthenticContext();
        }

        // GET: User
        public ActionResult Index()
        {
            var users = _context.Users
                .Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList();

            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

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
        public ActionResult Create(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            if (user.Password != "" && user.Password != user.PasswordRe)
            {
                ModelState.AddModelError(
                    "Password",
                    "As senhas devem ser iguais."
                );
                return View(user);
            }

            if (_context.Users.Any(x => x.Email == user.Email))
            {
                ModelState.AddModelError(
                    "Email",
                    "Este e-mail já está cadastrado."
                );
                return View(user);
            }

            if (_context.Users.Any(x => x.NickName == user.NickName))
            {
                ModelState.AddModelError(
                    "Email",
                    "Este NickName já está cadastrado."
                );
                return View(user);
            }

            try
            {
                var userDAO = new User() { Name = user.Name, NickName = user.NickName, Email = user.Email, PhoneNumber = user.PhoneNumber, PasswordHash = "WSD#%@$@%%" };
                _context.Users.Add(userDAO);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var userFound = _context.Users.FirstOrDefault(x => x.Id == id);

            if (userFound is null)
            {
                return HttpNotFound();
            }
            var user = new UserEditViewModel
            {
                Id = userFound.Id,
                Name = userFound.Name,
                NickName = userFound.NickName,
                Email = userFound.Email,
                PhoneNumber = userFound.PhoneNumber,
                IsBlocked = userFound.IsBlocked
            };
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserEditViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            if (user.Password!="" && user.Password != user.PasswordRe)
            {
                ModelState.AddModelError(
                    "Password",
                    "As senhas devem ser iguais."
                );
                return View(user);
            }

            if (_context.Users.Any(x => x.Id != user.Id && x.Email == user.Email))
            {
                ModelState.AddModelError(
                    "Email",
                    "Este e-mail já está cadastrado."
                );
                return View(user);
            }

            if (_context.Users.Any(x => x.Id != user.Id && x.NickName == user.NickName))
            {
                ModelState.AddModelError(
                    "NickName",
                    "Este NickName já está cadastrado."
                );
                return View(user);
            }
            
            var userDAO = _context.Users.FirstOrDefault(us => us.Id == id);
            if (userDAO is null)
            {
                return HttpNotFound();
            }

            try
            {

                userDAO.Name = user.Name;
                userDAO.NickName = user.NickName;
                userDAO.Email = user.Email;
                userDAO.PhoneNumber = user.PhoneNumber;
                userDAO.IsBlocked = user.IsBlocked;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var userFound = _context.Users.FirstOrDefault(x => x.Id == id);

            if (userFound is null)
            {
                return HttpNotFound();
            }
            var user = new UserEditViewModel
            {
                Id = userFound.Id,
                Email = userFound.Email,
                NickName = userFound.NickName,
                PhoneNumber = userFound.PhoneNumber,
                IsBlocked = userFound.IsBlocked
            };
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserEditViewModel user)
        {
            try
            {
                var userDAO = _context.Users.FirstOrDefault(us => us.Id == id);
                if (userDAO is null)
                {
                    return HttpNotFound();
                }

                _context.Users.Remove(userDAO);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
