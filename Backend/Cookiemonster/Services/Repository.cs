using Cookiemonster.Services;
using Microsoft.EntityFrameworkCore;

namespace Cookiemonster.Services
{
    public class Repository<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
