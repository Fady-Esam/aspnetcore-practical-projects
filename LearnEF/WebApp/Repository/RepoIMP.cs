using WebApp.Data;
using WebApp.Models;
using WebApp.Repository.Base;

namespace WebApp.Repository
{
    public class RepoIMP<T>: IRepo <T> where T : class
    {
        private AppDBContext context;
        public RepoIMP(AppDBContext context)
        {
            this.context = context;
        }
        public T FindeByID(int id)
        {
            return context.Set<T>().Find(id);
        }


        public IEnumerable<T> GetCategories()
        {
            return context.Set<T>().ToList();
        }

        public T GetItem(Func<T, bool> match)
        {
            return context.Set<T>().SingleOrDefault(match);
        }
    }
}
