using WebApp.Data;
using WebApp.Models;
using WebApp.Repository.Base;

namespace WebApp.Repository
{
    public class EmpRepoIMP : RepoIMP<Employee>, IEmpRepo
    {
        private readonly AppDBContext _context;
        public EmpRepoIMP(AppDBContext context) : base(context)
        {
            _context = context;
        }
        public void makeTitle()
        {
            throw new NotImplementedException();
        }

        public void setSalary()
        {
            throw new NotImplementedException();
        }
    }
}
