using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class UnitTypeRepository : StandardRepository<UnitType, UnitTypeModel>, IUnitTypeRepository
    {
        public UnitTypeRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }
    }
}