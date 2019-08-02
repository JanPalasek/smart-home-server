using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories.Interfaces
{
    public interface IStandardRepository<TModel>
        where TModel : Model
    {
        Task<ICollection<TModel>> GetAllAsync();
        Task<TModel> SingleAsync(long id);
        Task<TModel> SingleOrDefaultAsync(long id);
        Task<bool> AnyAsync(long id);

        Task DeleteAsync(long id);

        Task<long> AddOrUpdateAsync(TModel model);
    }
}