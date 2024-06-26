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

        public async Task<IActionResult> Index(string sterm="",int categoryId=0)
        {
            IEnumerable<Component> components = await _homeRepository.GetComponents(sterm, categoryId);
            IEnumerable<Category> categories = await _homeRepository.Categories();
            ComponentDisplayModel componentModel = new ComponentDisplayModel
            {
              Components=components,
              Categories=categories,
              STerm=sterm,
              CategoryId=categoryId
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