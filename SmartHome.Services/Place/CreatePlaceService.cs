using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Place;

namespace SmartHome.Services.Place
{
    public class CreatePlaceService : ICreatePlaceService
    {
        private readonly IPlaceRepository repository;

        public CreatePlaceService(IPlaceRepository repository)
        {
            this.repository = repository;
        }

        public Task<long> CreateAsync(PlaceModel model)
        {
            if (model.Id != 0)
            {
                throw new ArgumentException(nameof(model.Id));
            }

            return repository.AddOrUpdateAsync(model);
        }
    }
}