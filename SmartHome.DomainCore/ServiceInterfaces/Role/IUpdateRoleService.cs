using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.ServiceInterfaces.Role
{
    public interface IUpdateRoleService
    {
        Task<SmartHomeValidationResult> UpdateRoleAsync(RoleModel model);
        Task<SmartHomeValidationResult> UpdatePermissionsAsync(long roleId,
            IEnumerable<long> removedPermissions,
            IEnumerable<(long OldPermissionsId, string NewPermissionValue)> updatePermissions,
            IEnumerable<string> addedPermissions);
    }
}