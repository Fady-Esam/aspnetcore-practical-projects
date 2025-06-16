using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trips.Models;
using Trips.Models.ViewModels;

namespace Trips.Controllers
{
    public class TripController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TripController(ApplicationDbContext context)
        {
            _context = context;
        }
        // when id equal null, then you are in Add situation, but when Id has a value, then you are in update situation
        [HttpGet("[controller]/[action]/{id?}")]
        public IActionResult Add(int? id)
        {
            var tripVM = new TripVM();
            tripVM.PageNumber = 1;
            if (id.HasValue)
            {
                tripVM.Trip = _context.Trips.FirstOrDefault(x => x.Id == id) ?? new Trip();
            }
            return View(tripVM);
        }
        [HttpPost]
        public IActionResult Add(TripVM tripVM)
        {
            if (ModelState.IsValid)
            {
                if (tripVM.Trip.Id == 0)
                {
                    _context.Trips.Add(tripVM.Trip);
                }
                else
                {
                    _context.Trips.Update(tripVM.Trip);
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(tripVM);
        }
        public IActionResult Delete(TripVM tripVM)
        {
            if (tripVM == null || tripVM.Trip == null) return NotFound();
            _context.Trips.Remove(tripVM.Trip);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
