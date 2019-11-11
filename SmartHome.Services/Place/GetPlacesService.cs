using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Place;

namespace SmartHome.Services.Place
{
    public class GetPlacesService : IGetPlacesService
    {
        private readonly IPlaceRepository placeRepository;

        public GetPlacesService(IPlaceRepository placeRepository)
        {
            this.placeRepository = placeRepository;
        }

        public Task<IList<PlaceModel>> GetAllPlacesAsync()
        {
            return placeRepository.GetAllAsync();
        }

        public Task<PlaceModel> GetPlaceAsync(long id)
        {
            return placeRepository.GetByIdAsync(id);
        }
    }
}