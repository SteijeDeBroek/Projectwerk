namespace Cookiemonster.Interfaces
{
    public interface IRepository<T> where T : class, IDeletable
    {
        T Get(params int[] ids);

        public List<T> GetAll();

        public T Create(T entity);
        public T Update(T entity);

        public bool Delete(params int[] ids);
    }
}
