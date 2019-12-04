using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Permission;

namespace SmartHome.Services.Permission
{
    public class CreatePermissionService : ICreatePermissionService
    {
        private readonly IPermissionRepository repository;

        public CreatePermissionService(IPermissionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ServiceResult<long>> CreateAsync(PermissionModel model)
        {
            if (model.Id > 0)
            {
                throw new ArgumentException(nameof(model.Id), "Invalid model Id.");
            }

            if (await repository.GetByName(model.Name) != default)
            {
                var errors = SmartHomeValidationResult.Failed(
                    new SmartHomeValidation(nameof(model.Name),
                        $"Permission named {model.Name} already exists in the database."));
                
                return new ServiceResult<long>(default, errors);
            }

            long id = await repository.AddOrUpdateAsync(model);
            return new ServiceResult<long>(id, SmartHomeValidationResult.Success);
        }
    }
}