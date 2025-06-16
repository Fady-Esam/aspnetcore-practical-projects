using AuthProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99m, Category = "Electronics" },
            new Product { Id = 2, Name = "Smartphone", Price = 599.99m, Category = "Electronics" },
            new Product { Id = 3, Name = "Coffee Maker", Price = 49.99m, Category = "Home Appliances" },
            new Product { Id = 4, Name = "Desk Chair", Price = 149.99m, Category = "Furniture" },
            new Product { Id = 5, Name = "Running Shoes", Price = 89.99m, Category = "Sportswear" }
        };
            return Ok(products);
        }
    }
}
