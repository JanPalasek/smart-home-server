using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Role
{
    public interface IGetRolesService
    {
        Task<IList<RoleModel>> GetAllRolesAsync();

        Task<RoleModel> GetRoleByIdAsync(long roleId);

        Task<RoleModel> GetRoleByNameAsync(string name);

        Task<IList<RoleModel>> GetUserRolesAsync(long userId);
    }
}