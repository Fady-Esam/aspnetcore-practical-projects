
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IMPAsp.Controllers.CookiesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class CookieController : Controller
    {
        [HttpPost("set-cookie")]
        public IActionResult SetCookie(object cookieData)
        {
            var cookie = new CookieOptions
            {
                Path = "/",
                HttpOnly = true, // Prevent JavaScript access
                Secure = false,
                Expires = DateTime.Now.AddMinutes(10),
                SameSite = SameSiteMode.Strict,
            };
            string CookieKey = "IMPCookie";
            HttpContext.Response.Cookies.Append(CookieKey, JsonSerializer.Serialize(new {CookieData = cookieData}), cookie);
            return Ok(new { Message = "Cookie set successfully" });
        }
        [HttpGet("get-cookie")]
        public IActionResult GetCookie(string CookieKey)
        {
            if(HttpContext.Request.Cookies.TryGetValue(CookieKey, out var cookie))
            {
                if (cookie != null)
                    return Ok(cookie);
            }
            return NotFound();
        }
        [HttpDelete("delete-cookie")]
        public IActionResult DeleteCookie(string cookieName)
        {
            if (HttpContext.Request.Cookies.TryGetValue(cookieName, out var cookie))
            {
                if (cookie != null)
                {
                    HttpContext.Response.Cookies.Delete(cookieName);
                    return Ok(new { Message = "Cookie deleted successfully" });
                }
            }
            return NotFound();
        }
    }
}
