using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IRoleRepository
    {
        Task<IdentityResult> CreateRoleAsync(RoleModel model);

        Task<IdentityResult> UpdateRoleAsync(RoleModel model);

        Task<IdentityResult> DeleteRoleAsync(long roleId);
    }
}