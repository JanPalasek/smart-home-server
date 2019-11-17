using System.Threading.Tasks;

namespace SmartHome.DomainCore.ServiceInterfaces.BatteryPowerSourceType
{
    public interface IDeleteBatteryPowerSourceTypeService
    {
        Task DeleteAsync(long id);
    }
}