using WebApp.Models;

namespace WebApp.Repository.Base
{
    public interface IRepo<T> where T : class
    {
        public T FindeByID(int id);
        public IEnumerable<T> GetCategories();
        public T GetItem(Func<T, bool> match);
    }
}
