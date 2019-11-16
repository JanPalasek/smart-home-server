using System.Threading.Tasks;

namespace SmartHome.DomainCore.ServiceInterfaces.Place
{
    public interface IDeletePlaceService
    {
        Task DeletePlaceAsync(long id);
    }
}