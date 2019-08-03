using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartHome.Database;
using SmartHome.Repositories.Utils;
using SmartHome.Shared;

namespace SmartHome.Repositories
{
    /// <summary>
    /// Wrapper around <see cref="SmartHomeDbContext"/> that should be used instead of that class,
    /// because it wraps it functionality and provides additional methods to make usage of database easier.
    /// </summary>
    public class SmartHomeAppDbContext
    {
        protected internal SmartHomeDbContext Context { get; }

        public SmartHomeAppDbContext(SmartHomeDbContext context)
        {
            this.Context = context;
        }
        
        public async Task DeleteAsync<TEntity>(long id)
            where TEntity : class, IId<long>, new()
        {
            await DeleteAsync(new TEntity() { Id = id });
        }
        
        public async Task DeleteAsync<TEntity>(TEntity entity)
            where TEntity : class, IId<long>, new()
        {
            var dbEntity = SingleOrDefaultAsync<TEntity>(entity.Id);

            // didn't find anything => exception
            if (dbEntity == null)
            {
                throw new ArgumentException($"Entity of type {typeof(TEntity)} with id {entity.Id} is not in the database and thus cannot be deleted.");
            }

            Context.Remove(dbEntity);
            await Context.SaveChangesAsync();
        }
        
        public async Task DeleteRangeAsync<TEntity>(IEnumerable<TEntity> items)
            where TEntity : class, IId<long>, new()
        {
            bool originalAutoDetectChangesEnabled = Context
                .ChangeTracker.AutoDetectChangesEnabled;
            // set false, because ChangeTracker.Entries would be ineffective otherwise
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            try
            {
                var existingItems =
                    Context.ChangeTracker.Entries<TEntity>().Select(x => x.Entity);

                var entityEqualityComparer =
                    new EntityEqualityComparer();

                // entries that are being tracked (must delete the tracked object)
                var trackedItemsToDelete =
                    existingItems.Intersect(items,
                        entityEqualityComparer)
                        .ToList();

                // entities that are not tracked
                var otherItems = items.Except(existingItems,
                    entityEqualityComparer)
                    .ToList();

                Context.RemoveRange(trackedItemsToDelete);
                Context.RemoveRange(otherItems);

                await Context.SaveChangesAsync();
            }
            finally
            {
                // set to previous value (usually true, because we want to detect changes for updates to be more effective and update only some properties)
                Context.ChangeTracker.AutoDetectChangesEnabled = originalAutoDetectChangesEnabled;
            }
        }
        
        public async Task<TEntity> SingleAsync<TEntity>(long id)
            where TEntity : class, IId<long>, new()
        {
            var entity = await SingleOrDefaultAsync<TEntity>(id);

            if (entity == null)
            {
                throw new InvalidOperationException($"Entity of type {typeof(TEntity)} with id {id} does not exist in the database.");
            }

            return entity;
        }
        
        public async Task<TEntity> SingleOrDefaultAsync<TEntity>(long id)
            where TEntity :class, IId<long>, new()
        {
            var entity = await Context.FindAsync<TEntity>(id);
            
            return entity;
        }
        
        public async Task<long> AddOrUpdateAsync<TEntity>(TEntity entity)
            where TEntity : class, IId<long>, new()
        {
            var dbEntity = await SingleOrDefaultAsync<TEntity>(entity.Id);

            if (dbEntity == null)
            {
                await Context.AddAsync(entity);
            }
            else
            {
                // update values on db entity
                var entry = Context.Entry(dbEntity);
                entry.CurrentValues.SetValues(entity);
            }

            await Context.SaveChangesAsync();
            return entity.Id;
        }
        
        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> items)
            where TEntity : class, IId<long>, new()
        {
            await Context.AddRangeAsync(items);
            await Context.SaveChangesAsync();
        }
        
        public async Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> items)
            where TEntity : class, IId<long>, new()
        {
            var entityEqualityComparer = new EntityEqualityComparer();

            // entries that are already being tracked
            var existingEntries = Context.ChangeTracker
                .Entries<TEntity>().Select(x => x.Entity)
                .Intersect(items, entityEqualityComparer);

            // entities that are not tracked
            var others = items.Except(existingEntries,
                entityEqualityComparer);

            // update entities that are not being tracked
            Context.UpdateRange(others);

            await Context.SaveChangesAsync();
        }
        
        public IQueryable<TEntity> Query<TEntity>()
            where TEntity : class, IId<long>, new()
        {
            return Context.Set<TEntity>();
        }
    }
}