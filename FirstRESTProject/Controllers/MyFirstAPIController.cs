using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstRESTProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyFirstAPIController : ControllerBase
    {
        [HttpGet]
        public string Getame(string name)
        {
            return $"My Name Is {name}";
        }
        [HttpGet("sumNums")]
        public int Sum(int x, int y)
        {
            return x + y;
        }

    }
}
