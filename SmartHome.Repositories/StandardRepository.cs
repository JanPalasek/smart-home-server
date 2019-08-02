using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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

        public Task<ICollection<TModel>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<TModel> SingleAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<TModel> SingleOrDefaultAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> AddOrUpdateAsync(TModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}