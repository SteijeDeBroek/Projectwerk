using Cookiemonster.Models;

namespace Cookiemonster.Interfaces
{
    public interface IRepository<T> where T : class, IDeletable
    {
        public T Get(int id1, int id2 = 0);

        public List<T> GetAll();
        public List<Category> GetThreeLast();

        public T Create(T entity);
        public T Update(T entity);


        public bool Delete(int id1, int id2 = 0);
    }
}
