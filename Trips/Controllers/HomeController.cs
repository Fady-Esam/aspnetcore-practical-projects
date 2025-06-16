using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Trips.Models;

namespace Trips.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var trips = _context.Trips.ToList();
            return View(trips);
        }

    }
}
