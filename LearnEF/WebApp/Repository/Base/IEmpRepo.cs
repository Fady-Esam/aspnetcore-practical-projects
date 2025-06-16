using WebApp.Models;

namespace WebApp.Repository.Base
{
    public interface IEmpRepo : IRepo<Employee>
    {
        void setSalary();
        void makeTitle();
    }
}
