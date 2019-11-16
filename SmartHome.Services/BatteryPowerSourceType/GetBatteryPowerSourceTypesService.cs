using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.BatteryPowerSourceType;

namespace SmartHome.Services.BatteryPowerSourceType
{
    public class GetBatteryPowerSourceTypesService : IGetBatteryPowerSourceTypesService
    {
        private readonly IBatteryPowerSourceTypeRepository repository;

        public GetBatteryPowerSourceTypesService(IBatteryPowerSourceTypeRepository repository)
        {
            this.repository = repository;
        }

        public Task<IList<BatteryPowerSourceTypeModel>> GetAllPowerSourceTypesAsync()
        {
            return repository.GetAllAsync();
        }
    }
}