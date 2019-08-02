using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartHome.Database.Entities;
using SmartHome.Database.Repositories;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class StandardRepository<TEntity, TModel> : GenericRepository<TEntity>, IStandardRepository<TModel>
        where TEntity : Entity, new()
        where TModel : Model
    {
        protected StandardRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }

        public virtual async Task<ICollection<TModel>> GetAllAsync()
        {
            return await SmartHomeAppDbContext.Query<TEntity>().ProjectTo<TModel>(Mapper.ConfigurationProvider).ToListAsync();
        }

        public new async Task<TModel> SingleAsync(long id)
        {
            var entity = await base.SingleAsync(id);

            return Mapper.Map<TModel>(entity);
        }

        public new async Task<TModel> SingleOrDefaultAsync(long id)
        {
            var entity = await base.SingleOrDefaultAsync(id);

            return Mapper.Map<TModel>(entity);
        }

        public Task<long> AddOrUpdateAsync(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            return AddOrUpdateAsync(entity);
        }
    }
}