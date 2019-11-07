using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IStandardRepository<TModel>
        where TModel : class, IId<long>, new()
    {
        Task<ICollection<TModel>> GetAllAsync();
        Task<TModel> SingleAsync(long id);
        Task<TModel> SingleOrDefaultAsync(long id);
        Task<bool> AnyAsync(long id);

        Task DeleteAsync(long id);

        Task<long> AddOrUpdateAsync(TModel model);
    }
}