using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;

namespace NailSalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NailTypeController : Controller
    {
        private readonly INailTypeService _service;
        private readonly IWebHostEnvironment _environment;

        public NailTypeController(INailTypeService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var models = await _service.GetAllAsync();
            var viewModels = models.Select(x => new NailTypeVm
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                ImageUrl = x.ImageUrl
            }).ToList();

            return View(viewModels); 
        }

       
        public IActionResult Create() => View();

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NailTypeVm nailtypevm)
        {
            if (!ModelState.IsValid) return View(nailtypevm);

            var model = new NailType
            {
                Title = nailtypevm.Title,
                Price = nailtypevm.Price,
                ImageUrl = nailtypevm.ImageUrl
            };

            await _service.CreateAsync(model, nailtypevm.FormFile, _environment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null) return NotFound();

            var nailtypevm = new NailTypeVm
            {
                Id = model.Id,
                Title = model.Title,
                Price = model.Price,
                ImageUrl = model.ImageUrl
            };

            return View(nailtypevm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(NailTypeVm nailtypevm)
        {
            if (!ModelState.IsValid) return View(nailtypevm);

            var model = new NailType
            {
                Id = nailtypevm.Id,
                Title = nailtypevm.Title,
                Price = nailtypevm.Price,
                ImageUrl = nailtypevm.ImageUrl
            };

            await _service.UpdateAsync(model, nailtypevm.FormFile, _environment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id, _environment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }
    }
}
