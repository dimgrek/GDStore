using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GDStore.DAL.Interface.Services;

namespace GDStore.DAL.SQL.Services
{
    public class SQLRepository<T> : ISQLRepository<T>, IDisposable where T : class
    {
        private readonly DbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public SQLRepository()
        {
        }

        public SQLRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> criteria)
        {
            return await dbSet.SingleOrDefaultAsync(criteria);
        }

        public IQueryable<T> GetAll(int? take = null)
        {
            if (take.HasValue)
                return dbSet.Take(take.Value);

            return dbSet.AsQueryable();
        }

        public Task<IQueryable<T>> GetAllAsync(int? take = null)
        {
            if (take.HasValue)
                return Task.FromResult(dbSet.Take(take.Value));

            return Task.FromResult(dbSet.AsQueryable());
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> criteria, int? take = null)
        {
            if (take.HasValue)
                return dbSet.Where(criteria).Take(take.Value);

            return dbSet.Where(criteria);
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> criteria, int? take = null)
        {
            if (take.HasValue)
                return Task.FromResult(dbSet.Where(criteria).Take(take.Value));

            return Task.FromResult(dbSet.Where(criteria));
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public async Task<long> CountAsync()
        {
            return await dbSet.LongCountAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null)
                Delete(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}