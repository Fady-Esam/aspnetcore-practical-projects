using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StateManagementProject.Models;

namespace StateManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationDBContext _context;
        private const string cachKey = "ProductCache";
        public CacheController(IMemoryCache memoryCache, ApplicationDBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            if (_memoryCache.TryGetValue(cachKey, out List<Product>? result) && result is not null)
            {
                return Ok(new
                {
                    Message = "Products Found in Cache",
                    Products = result
                });
            }
            var products = _context.Products.ToList();
            _memoryCache.Set(cachKey, products, TimeSpan.FromMinutes(5));
            return Ok("Cache updated successfully.");
        }
    }
}
