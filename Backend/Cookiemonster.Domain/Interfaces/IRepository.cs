﻿using Cookiemonster.Infrastructure.EFRepository.Interfaces;

namespace Cookiemonster.Domain.Interfaces
{
    public interface IRepository<T> where T : class, IDeletable
    {
        public T? Get(int id1, int id2 = 0);

        public List<T> GetAll();
        public T Create(T entity);
        public T? Update(T entity, Func<T, object> keySelector);



        public bool Delete(int id1, int id2 = 0);

        public IQueryable<T> Queryable();


    }
}
