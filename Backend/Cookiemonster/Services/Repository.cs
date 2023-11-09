using Cookiemonster.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cookiemonster.Services
{
    public class Repository<T> where T : class, IDeletable
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Get(params int[] ids)
        {
            return _dbSet.Find(ids);
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

        public bool Delete(params int[] ids)
        {
            var entity = _dbSet.Find(ids);
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
