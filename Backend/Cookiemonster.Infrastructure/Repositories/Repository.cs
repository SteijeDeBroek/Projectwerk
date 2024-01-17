using Cookiemonster.Domain.Interfaces;
using Cookiemonster.Infrastructure.EFRepository.Context;
using Cookiemonster.Infrastructure.EFRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<T?> GetAsync(int id1, int id2 = 0)
        {
            T? entity;
            if (id2 == 0)
            {
                entity = await _dbSet.FindAsync(id1);
            }
            else
            {
                entity = await _dbSet.FindAsync(id1, id2);
            }
            if (entity?.IsDeleted == false)
            {
                return entity;
            }
            return null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.Where(entity => entity.IsDeleted == false).ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> UpdateAsync(T entity, Func<T, object> keySelector)
        {
            if (entity.IsDeleted == false)
            {
                // Get the primary key value
                var keyValue = keySelector(entity);

                // Check if the entity is already being tracked
                var existingEntity = await _dbSet.FindAsync(keyValue);

                if (existingEntity == null)
                {
                    // If not tracked, attach and set the state to Modified
                    _dbSet.Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id1, int id2 = 0)
        {
            T? entity;
            if (id2 == 0)
            {
                entity = await _dbSet.FindAsync(id1);
            }
            else
            {
                entity = await _dbSet.FindAsync(id1, id2);
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
            await _context.SaveChangesAsync();
            return true;
        }

    }
}