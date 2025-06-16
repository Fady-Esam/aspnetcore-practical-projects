using WebApp.Models;

namespace WebApp.Repository.Base
{
    public interface IUnitOfWork
    {
        public IEmpRepo Employees { get; }
        public IRepo<ProductModel> Products { get; }
        public IRepo<CategoryModel> Categories { get; }
        int commitChanges();
    }
}
