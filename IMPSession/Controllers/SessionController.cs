using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IMPSession.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : Controller
    {
        [HttpPost("add-session")]
        public IActionResult AddSession(object sessionData)
        {
            string SessionKey = "IMPSession";
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(new { SessionData = sessionData }));
            return Ok(new { Message = "Session added successfully" });
        }
        [HttpGet("get-session")]
        public IActionResult GetSession(string SessionKey)
        {
            var session = HttpContext.Session.GetString(SessionKey);
            if (session != null)
                return Ok(session);
            return NotFound();
        }
        [HttpGet("delete-session")]
        public IActionResult DeleteSession(string SessionKey)
        {
            var session = HttpContext.Session.GetString(SessionKey);
            if (session != null)
            {
                HttpContext.Session.Remove(SessionKey);
                return Ok(new { Message = "Session deleted successfully" });
            }
            return NotFound();
        }
    }
}
