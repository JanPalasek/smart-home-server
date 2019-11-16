using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Role
{
    public interface ICreateRoleService
    {
        Task<IdentityResult> CreateRoleAsync(CreateRoleModel model);
    }
}