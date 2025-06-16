using Microsoft.AspNetCore.Mvc;
using RouteApp.Models;
using System.Diagnostics;

namespace RouteApp.Controllers
{

    //[Route("retail/[controller]/[action]/{id?}")]
    //[Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            return Content($"Hello from Index Controller {id}");
        }

        public IActionResult Privacy()
        {
            return Content("Hello In Privacy");
        }
        public IActionResult CountDown(int id = 0)
        {
            string CountDown = "Count Down \n";
            for (int i = id; i >= 0; i--) CountDown += i + "\n";
            return Content(CountDown);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
