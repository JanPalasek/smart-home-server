using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IStandardRepository<TModel> : IGetByIdRepository<TModel>
        where TModel : class, IId<long>, new()
    {
        Task<IList<TModel>> GetAllAsync();
        Task<bool> AnyAsync(long id);

        Task DeleteAsync(long id);

        Task<long> AddOrUpdateAsync(TModel model);
    }
}