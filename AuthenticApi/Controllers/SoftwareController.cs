using Authentic_Api.Models.ViewModels;
using AuthenticApi.Services.SoftwareService;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AuthenticApi.Controllers
{
    public class SoftwareController : Controller
    {
        private readonly ISoftwareQueryService _softwareQueryService;
        private readonly ISoftwareCommandService _softwareCommandService;
        public SoftwareController(ISoftwareQueryService softwareQueryService, ISoftwareCommandService softwareCommandService)
        {
            _softwareQueryService = softwareQueryService;
            _softwareCommandService = softwareCommandService;
        }

        public async Task<ActionResult> Index()
        {
            var softwares= await _softwareQueryService.GetAllActives();
            return View(softwares);
        }

        public async Task<ActionResult> Details(int id)
        {
            var software = await _softwareQueryService.GetById(id);

            if (software is null)
            {
                return HttpNotFound();
            }
            return View(software);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SoftwareViewModel software)
        {
            if (!ModelState.IsValid)
                return View(software);

            try
            {
                await _softwareCommandService.Create(software);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var software = await _softwareQueryService.GetById(id);

            if (software is null)
            {
                return HttpNotFound();
            }

            return View(software);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SoftwareViewModel software)
        {
            if (!ModelState.IsValid)
                return View(software);


            var softwareDAO = await _softwareQueryService.GetById(id);
            if (softwareDAO is null)
            {
                return HttpNotFound();
            }

            try
            {
                await _softwareCommandService.Update(software);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var softwareFound = await _softwareQueryService.GetById(id);

            if (softwareFound is null)
            {
                return HttpNotFound();
            }
            return View(softwareFound);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, SoftwareViewModel software)
        {
            try
            {
                await _softwareCommandService.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}