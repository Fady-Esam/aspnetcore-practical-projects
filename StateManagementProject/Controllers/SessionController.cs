using Microsoft.AspNetCore.Mvc;
namespace StateManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : Controller
    {
        [HttpPost]
        public IActionResult SetSession(string data)
        {
            HttpContext.Session.SetString("myData", data);
            return Ok("Session Added Successfully");
        }
        [HttpGet]
        public IActionResult GetSession(string sessionKey)
        {
            var sessionData = HttpContext.Session.GetString(sessionKey);
            if(sessionData != null)
                return Ok($"Your Session Data is {sessionData}");
            return NotFound();
        }
    }
}
