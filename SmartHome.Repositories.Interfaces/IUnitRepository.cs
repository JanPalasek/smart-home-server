using System.Threading.Tasks;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories.Interfaces
{
    public interface IUnitRepository : IStandardRepository<UnitModel>
    {
        Task<bool> AnyWithBatteryPowerSourceAsync(long unitId);
    }
}