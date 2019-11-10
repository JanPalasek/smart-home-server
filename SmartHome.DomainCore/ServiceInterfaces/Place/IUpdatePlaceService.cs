using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Place
{
    public interface IUpdatePlaceService
    {
        Task UpdateAsync(PlaceModel model);
    }
}