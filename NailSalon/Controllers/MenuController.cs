using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using System.Threading.Tasks;

namespace NailSalon.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var foods = await _menuService.GetAllAsync();
            return View(foods); 
        }

        [HttpPost]
        public IActionResult Index(List<int> selectedItems)
        {
            if (selectedItems == null || !selectedItems.Any())
            {
                TempData["Message"] = "Zəhmət olmasa ən azı bir məhsul seçin.";
                return RedirectToAction("Index");
            }

            HttpContext.Session.SetString("SelectedMenuItems", string.Join(",", selectedItems));
            return RedirectToAction("Profile", "Account");
        }

    }
}
