using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IGetAllRepository<TModel> where TModel: class, IId<long>, new()
    {
        Task<IList<TModel>> GetAllAsync();
    }
}