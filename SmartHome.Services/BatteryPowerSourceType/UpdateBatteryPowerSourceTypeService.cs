using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.BatteryPowerSourceType;

namespace SmartHome.Services.BatteryPowerSourceType
{
    public class UpdateBatteryPowerSourceTypeService : IUpdateBatteryPowerSourceTypeService
    {
        private readonly IBatteryPowerSourceTypeRepository repository;

        public UpdateBatteryPowerSourceTypeService(IBatteryPowerSourceTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task UpdateAsync(BatteryPowerSourceTypeModel model)
        {
            if (model.Id <= 0)
            {
                throw new ArgumentException(nameof(model.Id));
            }
            
            await repository.AddOrUpdateAsync(model);
        }
    }
}