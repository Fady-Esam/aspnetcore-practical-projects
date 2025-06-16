using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Repository;
using WebApp.Repository.Base;



namespace WebApp.areas.Employees.Controllers
{
    public class CategoryController : Controller
    {
        //private IRepo<CategoryModel> _IRepo;
        private readonly IUnitOfWork _IUnitOfWork;

        public CategoryController(IUnitOfWork IUnitOfWork)
        {
            _IUnitOfWork = IUnitOfWork;
        }
        public IActionResult Index()
        {
            return View(_IUnitOfWork.Categories.GetCategories());
        }
        
    }
}
