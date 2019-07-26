namespace SmartHome.Database.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        protected SmartHomeAppDbContext SmartHomeAppDbContext { get; }

        protected GenericRepository(SmartHomeAppDbContext smartHomeAppDbContext)
        {
            SmartHomeAppDbContext = smartHomeAppDbContext;
        }

        public Task<TEntity> SingleOrDefaultAsync(long entityId)
        {
            return SmartHomeAppDbContext.SingleOrDefaultAsync<TEntity>(entityId);
        }

        public Task<TEntity> SingleAsync(long entityId)
        {
            return SmartHomeAppDbContext.SingleAsync<TEntity>(entityId);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return SmartHomeAppDbContext.Query<TEntity>().SingleOrDefaultAsync<TEntity>(predicate: predicate);
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return SmartHomeAppDbContext.Query<TEntity>().SingleAsync<TEntity>(predicate);
        }

        public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return SmartHomeAppDbContext.Query<TEntity>().FirstAsync(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return SmartHomeAppDbContext.Query<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<ICollection<TEntity>> GetAllEntitiesAsync()
        {
            return await SmartHomeAppDbContext.Query<TEntity>().ToListAsync();
        }

//        public async Task<ICollection<TEntity>> GetAllEntitiesAsync<TOrder>(Expression<Func<TEntity, TOrder>> sortExpression,
//            SortDirection sortDirection = SortDirection.Ascending)
//        {
//            var query = SmartHomeAppDbContext.Query<TEntity>();
//
//            if (sortDirection == SortDirection.Descending)
//            {
//                query = query.OrderByDescending(sortExpression);
//            }
//            else
//            {
//                query = query.OrderBy(sortExpression);
//            }
//
//
//            return await query.ToListAsync();
//        }
//
        public Task<bool> AnyAsync(long entityId)
        {
            return SmartHomeAppDbContext.Query<TEntity>().AnyAsync(x => x.Id == entityId);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return SmartHomeAppDbContext.Query<TEntity>().AnyAsync(predicate);
        }

        public Task DeleteAsync(TEntity entity)
        {
            return SmartHomeAppDbContext.DeleteAsync(entity);
        }

        public Task<long> AddOrUpdateAsync(TEntity entity)
        {
            return SmartHomeAppDbContext.AddOrUpdateAsync(entity);
        }
    }
}