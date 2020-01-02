using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Role;

namespace SmartHome.Services.Role
{
    public class UpdateRoleService : IUpdateRoleService
    {
        private readonly IRoleRepository repository;
        private readonly IPermissionRepository permissionRepository;

        public UpdateRoleService(IRoleRepository repository, IPermissionRepository permissionRepository)
        {
            this.repository = repository;
            this.permissionRepository = permissionRepository;
        }

        public async Task<SmartHomeValidationResult> UpdateRoleAsync(RoleModel model)
        {
            return SmartHomeValidationResult.FromIdentityResult(await repository.UpdateRoleAsync(model));
        }

        public async Task<SmartHomeValidationResult> UpdatePermissionsAsync(
            long roleId,
            IEnumerable<long> removedPermissions,
            IEnumerable<(long OldPermissionsId, string NewPermissionValue)> updatePermissions,
            IEnumerable<string> addedPermissions)
        {
            removedPermissions = updatePermissions.Select(x => x.OldPermissionsId).Union(removedPermissions).ToList();
            await repository.RemovePermissionsFromRoleAsync(roleId, removedPermissions.ToList());
            
            var permissions = await permissionRepository.GetRolePermissionsAsync(roleId);
            var permissionNames = permissions.Select(x => x.Name);
            
            // TODO: update permissions
            addedPermissions = updatePermissions.Select(x => x.NewPermissionValue)
                .Union(addedPermissions)
                .Except(permissionNames);
            await repository.AddPermissionsToRoleAsync(roleId, addedPermissions.ToList());
            
            return SmartHomeValidationResult.Success;
        }
    }
}