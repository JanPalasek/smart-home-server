using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.BatteryPowerSourceType
{
    public interface IGetBatteryPowerSourceTypesService
    {
        Task<IList<BatteryPowerSourceTypeModel>> GetAllPowerSourceTypesAsync();
        Task<BatteryPowerSourceTypeModel> GetByIdAsync(long id);
    }
}