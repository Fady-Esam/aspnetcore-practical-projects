using IMPAsp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace IMPAsp.Controllers.CacheControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistributedCacheController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ApplicationDBContext _context;
        public DistributedCacheController(IDistributedCache distributedCache, ApplicationDBContext context)
        {
            _distributedCache = distributedCache;
            _context = context;
        }
        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            string cacheKey = "SetCacheKey";
            var cacheValue = await _distributedCache.GetStringAsync(cacheKey);

            if (cacheValue != null)
            {
                var data = JsonSerializer.Deserialize<List<Product>>(cacheValue);
                return Ok(new { Message = "Products Found in Cache", Products = data });
            }

            var products = _context.Proudcts.ToList();
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };
            string prod = JsonSerializer.Serialize(products);
            await _distributedCache.SetStringAsync(cacheKey, prod, options);
            return Ok(products);

        }
        [HttpDelete]
        public async Task<IActionResult> ClearCache()
        {
            string cacheKey = "SetCacheKey";
            await _distributedCache.RemoveAsync(cacheKey);
            return Ok(new { Message = "Cache Cleared" });
        }
    }
}
