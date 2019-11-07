using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Infrastructure
{
    public class PlaceRepository : StandardRepository<Place, PlaceModel>, IPlaceRepository
    {
        public PlaceRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }
    }
}