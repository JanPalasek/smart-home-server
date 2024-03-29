using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Place
{
    public interface IGetPlacesService
    {
        Task<IList<PlaceModel>> GetAllPlacesAsync();
        Task<PlaceModel> GetPlaceAsync(long id);
    }
}