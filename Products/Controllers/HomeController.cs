using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace ItLegenProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Ok("Hello In my New Project");
        }
    }
}
