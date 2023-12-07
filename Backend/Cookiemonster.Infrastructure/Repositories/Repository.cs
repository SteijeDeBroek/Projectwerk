using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookiemonster.Infrastructure.Repositories
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
            }
            else
            {
                entity = _dbSet.Find(id1, id2);
            }
            if (entity?.IsDeleted == false)
            {
                return entity;
            }
            return null;
        }

        public List<T> GetAll()
        {
            return _dbSet.Where(entity => entity.IsDeleted == false).ToList();
        }

        public IQueryable<Category> GetThreeLast()
        {
            if (typeof(T) == typeof(Category))
            {
                DbSet<Category> newDbSet = _dbSet as DbSet<Category>;
                return newDbSet.Where(entity => !entity.IsDeleted)
                               .OrderByDescending(entity => entity.StartDate)
                               .Take(3);
            }
            return null;
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            if (entity.IsDeleted == false)
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
            if (entity == null || entity.IsDeleted == true)
                return false;

            if (entity.IsDeletable)
            {
                _dbSet.Remove(entity);
            }
            else
            {
                entity.IsDeleted = true;
            }
            _context.SaveChanges();
            return true;
        }

        public IQueryable<T> Queryable()
        {
            return _dbSet.AsQueryable();  
        }
    }
}
