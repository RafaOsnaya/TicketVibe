using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketVibe.Entities;

namespace TicketVibe.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly DbContext context;

        protected RepositoryBase(DbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>()
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy)
        {
            return await context.Set<TEntity>()
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>()
                .FindAsync(id);
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>()
                .AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item is not null)
            {
                item.Status = false;
                await UpdateAsync();
            }
        }
    }
}
