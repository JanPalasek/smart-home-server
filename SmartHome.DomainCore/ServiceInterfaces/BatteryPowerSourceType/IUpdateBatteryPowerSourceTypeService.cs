using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.BatteryPowerSourceType
{
    public interface IUpdateBatteryPowerSourceTypeService
    {
        Task UpdateAsync(BatteryPowerSourceTypeModel model);
    }
}