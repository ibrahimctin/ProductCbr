﻿using System.Linq.Expressions;

namespace ProductCbrAssignment.Infrastructure.Repositories.Abstractions
{
    public interface IRepository<T> where T : class, new()
    {
        Task AddAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetEntityAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetById(string id);
        Task<T> Update(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}