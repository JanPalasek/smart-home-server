using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.ServiceInterfaces.Role
{
    public interface IUpdateRoleService
    {
        Task<SmartHomeValidationResult> UpdateRoleAsync(RoleModel model);
    }
}