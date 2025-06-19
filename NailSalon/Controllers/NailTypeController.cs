using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class NailTypeController : Controller
    {
        INailTypeService _nailTypeService;

        public NailTypeController(INailTypeService nailTypeService)
        {
            _nailTypeService = nailTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var nailTypes = await _nailTypeService.GetAllAsync(); 

            var nailTypeVms = nailTypes.Select(x => new NailTypeVm
            {
                Id = x.Id,
                Title = x.Title,
                Price = (decimal)x.Price,
                ImageUrl = x.ImageUrl
            }).ToList();

            return View(nailTypeVms);
        }

    }
}
