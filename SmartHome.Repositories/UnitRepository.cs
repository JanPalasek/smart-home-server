using System.Threading.Tasks;
using AutoMapper;
using SmartHome.Database.Entities;
using SmartHome.Database.Repositories;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class UnitRepository : StandardRepository<Unit, UnitModel>, IUnitRepository
    {
        public UnitRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper) : base(smartHomeAppDbContext, mapper)
        {
        }

        public Task<bool> AnyWithBatteryPowerSourceAsync(long unitId)
        {
            return AnyAsync(x => x.Id == unitId && x.BatteryPowerSourceTypeId != null);
        }
    }
}