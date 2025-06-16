using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StateManagementProject.Models;

namespace StateManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributedCacheController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ApplicationDBContext _context;
        private const string cachKey = "ProductCache";
        public DistributedCacheController(IDistributedCache distributedCache, ApplicationDBContext context)
        {
            _distributedCache = distributedCache;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            string cacheValue = await _distributedCache.GetStringAsync(cachKey);
            if (!string.IsNullOrEmpty(cacheValue))
            {
                return Ok(new { Message = "Products Fetched Successfully", Products = JsonConvert.DeserializeObject(cacheValue) });
            }
            var products = _context.Products.ToList();
            await _distributedCache.SetStringAsync(cachKey, JsonConvert.SerializeObject(products), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(50)
            });

            return Ok(products);
        }
    }
}
