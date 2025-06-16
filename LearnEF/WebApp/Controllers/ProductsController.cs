using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Models;
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDBContext _AppDBContext;
        private readonly IWebHostEnvironment _host;
        public ProductsController(AppDBContext AppDBContext, IWebHostEnvironment host)
        {
            _AppDBContext = AppDBContext;
            _host = host;
        }
        public IActionResult Index()
        {
            IEnumerable<ProductModel> data = _AppDBContext.Products.ToList();
            return View(data);
        }
        // Get
        public IActionResult AddNewItem(int id)
        {
            createSelectedList();
            if(id != 0)
            {
                ProductModel? ExistingProduct = _AppDBContext.Products.Find(id);
                if (ExistingProduct != null)
                    return View(ExistingProduct);
            }
            return View();
        }

        public void createSelectedList(int selectId = 0)
        {
            List<CategoryModel> categories = new List<CategoryModel>
            {
                new CategoryModel {Id = 1, CategoryName = "Electronics"},
                new CategoryModel {Id = 2, CategoryName = "Furniture"},
                new CategoryModel {Id = 3, CategoryName = "Clothing"},
                new CategoryModel {Id = 4, CategoryName = "Books"},
                new CategoryModel {Id = 5, CategoryName = "Sports Equipment"}
            };
            ViewBag.CategoryList = new SelectList(categories, "Id", "CategoryName", selectId);
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewItem(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.ID == 0)
                {
                    if (model.ClientFile != null)
                    {
                        string upload = Path.Combine(_host.WebRootPath, "images");
                        string fileName = model.ClientFile.FileName;
                        string fullPath = Path.Combine(upload, fileName);
                        model.ClientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
                        model.imagePath = fileName;
                    }
                    _AppDBContext.Products.Add(model);
                }
                else
                {
                    var ExistingProduct = _AppDBContext.Products.Find(model.ID);
                    if(ExistingProduct != null)
                    {
                        ExistingProduct.Name = model.Name;
                        ExistingProduct.Price = model.Price;
                        ExistingProduct.CategoryId = model.CategoryId;

                    }
                }

                _AppDBContext.SaveChanges();
                TempData["SuccessMessage"] =  $"Item has been {(model.ID == 0 ? "Added" : "Edited")}  Successfully";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult DeleteItem(int ID)
        {
            var ExistingProduct = _AppDBContext.Products.Find(ID);
            if(ExistingProduct != null)
            {
                _AppDBContext.Products.Remove(ExistingProduct);
                _AppDBContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
