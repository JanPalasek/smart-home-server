using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Permission;

namespace SmartHome.Services.Permission
{
    public class UpdatePermissionService : IUpdatePermissionService
    {
        private readonly IPermissionRepository repository;

        public UpdatePermissionService(IPermissionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SmartHomeValidationResult> UpdateAsync(PermissionModel model)
        {
            if (model.Id <= 0)
            {
                throw new ArgumentException(nameof(model.Id), "Invalid model Id.");
            }

            var dbModel = await repository.GetByName(model.Name);
            // if we don't change anything => return success
            if (dbModel != default && dbModel.Id == model.Id)
            {
                return SmartHomeValidationResult.Success;
            }
            
            // if we change something and the new name is already in the database (in different entity) => error
            if (dbModel != default)
            {
                return SmartHomeValidationResult.Failed(
                    new SmartHomeValidation(nameof(model.Name),
                        $"Permission named {model.Name} already exists in the database."));
            }

            await repository.AddOrUpdateAsync(model);
            return SmartHomeValidationResult.Success;
        }
    }
}