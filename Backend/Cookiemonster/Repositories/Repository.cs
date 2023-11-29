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
            T? entity;
            if (id2 == 0)
            {
                entity = _dbSet.Find(id1);
            } else
            {
                entity = _dbSet.Find(id1, id2);
            }
            if (entity?.isDeleted == false)
            {
                return entity;
            }
            return null;
        }

        public List<T> GetAll()
        {
            return _dbSet.Where(entity => entity.isDeleted == false).ToList();
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity.isDeleted == false)
            {
                _dbSet.Update(entity);
                _context.SaveChanges();
                return entity;
            }
            return null;
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
            if (entity == null || entity.isDeleted == true)
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
