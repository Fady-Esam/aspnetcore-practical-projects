using WebApp.Data;
using WebApp.Models;
using WebApp.Repository.Base;

namespace WebApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;

        public IEmpRepo Employees { get; private set; }

        public IRepo<ProductModel> Products { get; private set; }

        public IRepo<CategoryModel> Categories { get; private set; }

        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            Employees = new EmpRepoIMP(_context);
            Products = new RepoIMP<ProductModel>(_context);
            Categories = new RepoIMP<CategoryModel>(_context);
        }
        public int commitChanges()
        {
            return _context.SaveChanges();
        }
    }
}
