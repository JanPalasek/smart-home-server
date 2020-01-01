using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.User;

namespace SmartHome.Services.User
{
    public class UpdateUserService : IUpdateUserService
    {
        private readonly IUserRepository repository;
        private readonly IRoleRepository roleRepository;
        private readonly IPermissionRepository permissionRepository;

        public UpdateUserService(IUserRepository repository, IRoleRepository roleRepository, IPermissionRepository permissionRepository)
        {
            this.repository = repository;
            this.roleRepository = roleRepository;
            this.permissionRepository = permissionRepository;
        }

        public async Task<SmartHomeValidationResult> UpdateUserAsync(UserModel model)
        {
            return SmartHomeValidationResult.FromIdentityResult(await repository.UpdateUserAsync(model));
        }

        public async Task<SmartHomeValidationResult> AddToOrRemoveFromRolesAsync(long userId, IEnumerable<long> roleIds)
        {
            var allUserRoles = await roleRepository.GetUserRolesAsync(userId);

            var rolesToAdd = roleIds.Except(allUserRoles.Select(x => x.Id)).ToList();
            // roles that user currently has without those sent to the server
            var rolesToRemove = allUserRoles.Select(x => x.Id).Except(roleIds).ToList();
            
            if (rolesToAdd.Count > 0 || rolesToRemove.Count > 0)
            {
                var validationResult = SmartHomeValidationResult.FromIdentityResult(await repository.AddToRolesAsync(userId, rolesToAdd));
                validationResult = validationResult.Merge(SmartHomeValidationResult.FromIdentityResult(
                    await repository.RemoveFromRolesAsync(userId, rolesToRemove)));
                return validationResult;
            }
            
            return SmartHomeValidationResult.Success;
        }

        public async Task<SmartHomeValidationResult> UpdatePermissionsAsync(long userId,
            IEnumerable<long> removedPermissions,
            IEnumerable<(long OldPermissionsId, string NewPermissionValue)> updatePermissions,
            IEnumerable<string> addedPermissions)
        {
            // TODO: transaction
            removedPermissions = updatePermissions.Select(x => x.OldPermissionsId).Union(removedPermissions).ToList();
            await repository.RemovePermissionsFromUserAsync(userId, removedPermissions.ToList());
            
            
            var permissions = await permissionRepository.GetUserOnlyPermissionsAsync(userId);
            var permissionNames = permissions.Select(x => x.Name);
            
            // TODO: update permissions
            //
            addedPermissions = updatePermissions.Select(x => x.NewPermissionValue)
                .Union(addedPermissions)
                // except for permission names that are already used in the database
                .Except(permissionNames);
            await repository.AddPermissionsToUserAsync(userId, addedPermissions.ToList());
            
            return SmartHomeValidationResult.Success;
        }
    }
}