using Microsoft.AspNetCore.Mvc;

namespace NailSalon.Areas.Admin
{

    [Area("Admin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
