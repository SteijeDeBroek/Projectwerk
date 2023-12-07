using Cookiemonster.Infrastructure.EFRepository.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;

namespace Cookiemonster.Domain.Interfaces
{
    public interface IRepository<T> where T : class, IDeletable
    {
        public T Get(int id1, int id2 = 0);

        public List<T> GetAll();
        public T Create(T entity);
        public T Update(T entity);


        public bool Delete(int id1, int id2 = 0);

        IQueryable<T> Queryable();


    }
}
