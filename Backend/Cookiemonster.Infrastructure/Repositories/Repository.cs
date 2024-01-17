using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookiemonster.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IDeletable
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity?.IsDeleted == false ? entity : null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.Where(entity => !entity.IsDeleted).ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> UpdateAsync(T entity, Func<T, object> keySelector)
        {
            if (!entity.IsDeleted)
            {
                var keyValue = keySelector(entity);
                var existingEntity = await _dbSet.FindAsync(keyValue);
                if (existingEntity == null)
                {
                    _dbSet.Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null || entity.IsDeleted)
                return false;

            if (entity.IsDeletable)
            {
                _dbSet.Remove(entity);
            }
            else
            {
                entity.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<T> Queryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
