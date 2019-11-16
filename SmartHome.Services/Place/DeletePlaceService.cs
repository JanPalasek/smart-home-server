using System.Threading.Tasks;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Place;

namespace SmartHome.Services.Place
{
    public class DeletePlaceService : IDeletePlaceService
    {
        private readonly IPlaceRepository repository;

        public DeletePlaceService(IPlaceRepository repository)
        {
            this.repository = repository;
        }

        public async Task DeletePlaceAsync(long id)
        {
            await repository.DeleteAsync(id);
        }
    }
}