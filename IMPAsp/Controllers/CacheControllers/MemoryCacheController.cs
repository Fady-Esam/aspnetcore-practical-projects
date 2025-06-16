using IMPAsp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace IMPAsp.Controllers.CacheControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemoryCacheController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationDBContext _context;

        public MemoryCacheController(IMemoryCache memoryCache, ApplicationDBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        [HttpGet("get-products")]
        public IActionResult GetProducts()
        {
            string cacheKey = "SetCacheKey";
            if (_memoryCache.TryGetValue(cacheKey, out List<Product>? result) && result is not null)
            {
                return Ok(new {Message = "Products Found in Cache", Products = result});
            }
            var products = _context.Proudcts.ToList();
            _memoryCache.Set(cacheKey, products, TimeSpan.FromHours(1));
            return Ok(products);
        }
    }
}
