using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;
using NailSalon.Core.ViewModels.NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.CreateAsync(vm);
            TempData["Success"] = "Mesajınız göndərildi 💌";
            return RedirectToAction("Index");
        }
    }

}
