using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IPermissionRepository : IStandardRepository<PermissionModel>
    {
        Task<IList<PermissionModel>> GetUserOnlyPermissionsAsync(long userId);
        Task<IList<PermissionModel>> GetAllUserPermissionsAsync(long userId);
        Task<IList<PermissionModel>> GetRolePermissionsAsync(long roleId);
        Task<PermissionModel?> GetByNameAsync(string name);
        Task<IList<PermissionRoleModel>> GetAllUserPermissionsWithRolesAsync(long userId);
    }
}