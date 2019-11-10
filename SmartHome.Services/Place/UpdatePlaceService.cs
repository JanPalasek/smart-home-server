using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Place;

namespace SmartHome.Services.Place
{
    public class UpdatePlaceService : IUpdatePlaceService
    {
        private readonly IPlaceRepository placeRepository;

        public UpdatePlaceService(IPlaceRepository placeRepository)
        {
            this.placeRepository = placeRepository;
        }

        public async Task UpdateAsync(PlaceModel model)
        {
            if (model.Id == 0 || !(await placeRepository.AnyAsync(model.Id)))
            {
                throw new ArgumentException(nameof(model.Id));
            }
            
            await placeRepository.AddOrUpdateAsync(model);
        }
    }
}