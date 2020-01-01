using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IPermissionRepository : IStandardRepository<PermissionModel>
    {
        Task<IList<PermissionModel>> GetUserOnlyPermissionsAsync(long userId);
        
        /// <summary>
        /// Obtains all permissions that belong to specified user and his roles.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<PermissionModel>> GetAllUserPermissionsDistinctAsync(long userId);
        
        /// <summary>
        /// Obtains all permissions that specified role has.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IList<PermissionModel>> GetRolePermissionsAsync(long roleId);
        Task<PermissionModel?> GetByNameAsync(string name);
        
        /// <summary>
        /// Obtains all permissions that belong to specified user and his roles. Does not remove duplicates.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<PermissionRoleModel>> GetAllUserPermissionsWithRolesAsync(long userId);
    }
}