using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.BatteryPowerSourceType;

namespace SmartHome.Services.BatteryPowerSourceType
{
    public class CreateBatteryPowerSourceTypeService : ICreateBatteryPowerSourceTypeService
    {
        private readonly IBatteryPowerSourceTypeRepository repository;

        public CreateBatteryPowerSourceTypeService(IBatteryPowerSourceTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<long> CreateAsync(BatteryPowerSourceTypeModel model)
        {
            if (model.Id > 0)
            {
                throw new ArgumentException(nameof(model.Id));
            }
            
            return await repository.AddOrUpdateAsync(model);
        }
    }
}