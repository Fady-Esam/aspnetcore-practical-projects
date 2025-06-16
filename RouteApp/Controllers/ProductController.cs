using Microsoft.AspNetCore.Mvc;

namespace RouteApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult List(string id = "All")
        {
            return Content($"These are List of Products {id}");
        }
        public IActionResult Detail(int id)
        {
            return Content($"Get Details of the product => {id}");
        }
        [Route("Product/List/{id}/page{pageNum}/sort-by-{sortBy}")]
        public IActionResult List(int id, int pageNum, string sortBy)
        {
            return Content($"Get Details of the product with Id {id}, and pageNumber {pageNum} Sorted By {sortBy}");
        }
    }
}
