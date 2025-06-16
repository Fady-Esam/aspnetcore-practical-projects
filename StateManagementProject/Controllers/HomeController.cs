using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace StateManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpPost("set-cookie")]
        public IActionResult SetCookie(object value)
        {
            var data = JsonSerializer.Serialize(value);

            var cookie = new CookieOptions
            {
                Secure = true, // https
                Expires = DateTime.Now.AddDays(1)
            };
            HttpContext.Session.SetString("myData", data);
            var sessionId = HttpContext.Session.Id;

            Response.Cookies.Append("MyCookie", sessionId, cookie);
            return Ok("Cookie Added Successfully");
        }
        [HttpGet("get-cookie")]
        public IActionResult GetCookie(string cookieKey)
        {
            var data = Request.Cookies[cookieKey];
            if(data != null)
                return Ok($"The cookies are {data}");
            return NotFound("No cookies found");
        }
        [HttpGet("get-session")]
        public IActionResult GetSession(string sessionKey)
        {
            var data = HttpContext.Session.GetString(sessionKey);
            if (data != null)
                return Ok($"The Session Data is {data}");
            return NotFound("No Session Data found");
        }
        [HttpGet("delete-cookie")]
        public IActionResult DeleteCookie(string cookieKey)
        {
            var cookie = Request.Cookies[cookieKey];
            if(cookie != null)
            {
                Response.Cookies.Delete(cookieKey);
                return Ok("Cookie Deleted Successfully");
            }
            return BadRequest();
        }
    }
}
