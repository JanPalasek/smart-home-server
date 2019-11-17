using System.Threading.Tasks;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.BatteryPowerSourceType;

namespace SmartHome.Services.BatteryPowerSourceType
{
    public class DeleteBatteryPowerSourceTypeService : IDeleteBatteryPowerSourceTypeService
    {
        private readonly IBatteryPowerSourceTypeRepository repository;

        public DeleteBatteryPowerSourceTypeService(IBatteryPowerSourceTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id);
        }
    }
}