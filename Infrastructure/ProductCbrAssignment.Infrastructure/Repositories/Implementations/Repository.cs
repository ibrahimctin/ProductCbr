using Microsoft.EntityFrameworkCore;
using ProductCbrAssignment.Infrastructure.DbContext;
using ProductCbrAssignment.Infrastructure.Repositories.Abstractions;
using System.Linq.Expressions;

namespace ProductCbrAssignment.Infrastructure.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;

        }
        private DbSet<T> Table { get => _context.Set<T>(); }
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await Table.CountAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));

        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            if (predicate != null)
                query = query.Where(predicate);
            if (includeProperties.Any())
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                };

            return await query.ToListAsync();

        }

        public async Task<T> GetById(string id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);
            if (includeProperties.Any())
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                };

            return await query.SingleAsync();
        }

        public async Task<T> Update(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }
    }
}
