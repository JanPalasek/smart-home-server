using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.Database.Entities;

namespace SmartHome.Database.Repositories
{
    using System;
    using System.Linq.Expressions;
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        Task<bool> AnyAsync(long entityId);
        Task<ICollection<TEntity>> GetAllEntitiesAsync();
//        Task<ICollection<TEntity>> GetAllEntitiesAsync<TOrder>(Expression<Func<TEntity, TOrder>> sortExpression,
//            SortDirection sortDirection = SortDirection.Ascending);
        Task<TEntity> SingleAsync(long entityId);
        Task<TEntity> SingleOrDefaultAsync(long entityId);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task DeleteAsync(TEntity entity);

        Task<long> AddOrUpdateAsync(TEntity entity);
    }
}