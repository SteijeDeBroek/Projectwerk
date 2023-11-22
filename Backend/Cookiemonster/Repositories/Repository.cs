using Cookiemonster.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cookiemonster.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IDeletable
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Get(int id1, int id2 = 0)
        {
            if (id2 == 0)
            {
                return _dbSet.Find(id1);
            } else
            {
                return _dbSet.Find(id1, id2);
            }
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

        public bool Delete(int id1, int id2 = 0)
        {
            T? entity;
            if (id2 == 0)
            {
                entity = _dbSet.Find(id1);
            }
            else
            {
                entity = _dbSet.Find(id1, id2);
            }
            if (entity == null)
                return false;

            if (entity.isDeletable)
            {
                _dbSet.Remove(entity);
            } else
            {
                entity.isDeleted = true;
            }
            _context.SaveChanges();
            return true;
        }
    }
}
