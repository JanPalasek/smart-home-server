using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.BatteryPowerSourceType
{
    public interface ICreateBatteryPowerSourceTypeService
    {
        Task<long> CreateAsync(BatteryPowerSourceTypeModel model);
    }
}