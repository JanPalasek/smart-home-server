using System.Threading.Tasks;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IGetByIdRepository<TModel>
        where TModel: class, IId<long>, new()
    {
        Task<TModel> GetByIdAsync(long id);
    }
}