using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Permission
{
    public interface IGetPermissionsService
    {
        Task<IList<PermissionModel>> GetPermissionsAsync(long userId);
        Task<IList<PermissionModel>> GetAllPermissionsAsync();
        Task<PermissionModel> GetByIdAsync(long id);
    }
}