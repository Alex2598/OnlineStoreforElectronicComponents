using OnlineStoreforElectronicComponents.Models;
using OnlineStoreforElectronicComponents.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace OnlineStoreforElectronicComponents.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm="",int typeOfComponentId=0)
        {
            IEnumerable<Component> components = await _homeRepository.GetComponents(sterm, typeOfComponentId);
            IEnumerable<TypeOfComponent> genres = await _homeRepository.TypeOfComponents();
            ComponentDisplayModel componentModel = new ComponentDisplayModel
            {
              Components=components,
              TypeOfComponents=genres,
              STerm=sterm,
              TypeOfComponentId=typeOfComponentId
            };
            return View(componentModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}