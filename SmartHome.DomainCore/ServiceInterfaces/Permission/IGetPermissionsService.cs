using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Permission
{
    public interface IGetPermissionsService
    {
        Task<IList<PermissionModel>> GetUserOnlyPermissionsAsync(long userId);
        Task<IList<PermissionModel>> GetAllUserPermissionsAsync(string userName);
        Task<IList<PermissionModel>> GetRolePermissionsAsync(long roleId);
        Task<IList<PermissionModel>> GetAllPermissionsAsync();
        Task<PermissionModel> GetByIdAsync(long id);
        Task<IList<PermissionRoleModel>> GetAllUserPermissionsWithRolesAsync(long userId);
    }
}