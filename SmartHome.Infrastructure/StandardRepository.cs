using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SmartHome.DomainCore;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Infrastructure
{
    public class StandardRepository<TEntity, TModel> : GenericRepository<TEntity>, IStandardRepository<TModel>
        where TEntity : class, IId<long>, new()
        where TModel : class, IId<long>, new()
    {
        protected StandardRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }

        public virtual async Task<IList<TModel>> GetAllAsync()
        {
            return await SmartHomeAppDbContext.Query<TEntity>().ProjectTo<TModel>(Mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TModel> GetByIdAsync(long id)
        {
            var entity = await base.SingleAsync(id);

            return Mapper.Map<TModel>(entity);
        }

        public Task<long> AddOrUpdateAsync(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            return AddOrUpdateAsync(entity);
        }
    }
}