using SmartHome.Database.Entities;

namespace SmartHome.Database.Repositories
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        public UnitRepository(SmartHomeAppDbContext smartHomeAppDbContext) : base(smartHomeAppDbContext)
        {
        }
    }
}