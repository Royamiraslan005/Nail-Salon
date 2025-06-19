using Microsoft.AspNetCore.Mvc;
using NailSalon.DAL.Contexts;

namespace NailSalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactAdminController : Controller
    {
        private readonly AppDbContext _context;

        public ContactAdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var messages = _context.ContactMessages.OrderByDescending(x => x.CreatedAt).ToList();
            return View(messages);
        }
    }

}
