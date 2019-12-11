using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.ServiceInterfaces.User
{
    public interface IUpdateUserService
    {
        Task<SmartHomeValidationResult> UpdateUserAsync(UserModel model);
        Task<SmartHomeValidationResult> AddToOrRemoveFromRolesAsync(long userId, IEnumerable<long> roleIds);

        Task<SmartHomeValidationResult> UpdatePermissionsAsync(long userId,
            IEnumerable<long> removedPermissions,
            IEnumerable<(long OldPermissionsId, string NewPermissionValue)> updatePermissions,
            IEnumerable<string> addedPermissions);
    }
}