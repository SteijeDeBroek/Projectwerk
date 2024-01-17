using Cookiemonster.Infrastructure.EFRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookiemonster.Domain.Interfaces
{
    public interface IRepository<T> where T : class, IDeletable
    {
        Task<T?> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task<T?> UpdateAsync(T entity, Func<T, object> keySelector);
        Task<bool> DeleteAsync(int id);
        IQueryable<T> Queryable(); 
    }
}
