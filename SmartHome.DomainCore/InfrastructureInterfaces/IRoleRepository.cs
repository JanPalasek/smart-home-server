using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IRoleRepository : IGetAllRepository<RoleModel>, IGetByIdRepository<RoleModel>
    {
        Task<IdentityResult> CreateRoleAsync(CreateRoleModel model);

        Task<IdentityResult> UpdateRoleAsync(RoleModel model);

        Task<IdentityResult> DeleteRoleAsync(long roleId);

        Task<IList<EntityReference>> GetAllRoleReferences();

        Task<RoleModel> GetByNameAsync(string name);
        
        Task<IList<RoleModel>> GetUserRolesAsync(long userId);
        
        Task AddPermissionsToRoleAsync(long roleId, List<string> permissions);

        Task RemovePermissionsFromRoleAsync(long roleId, List<long> permission);
    }
}