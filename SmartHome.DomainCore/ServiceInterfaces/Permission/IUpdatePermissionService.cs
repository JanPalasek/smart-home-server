using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.ServiceInterfaces.Permission
{
    public interface IUpdatePermissionService
    {
        Task<SmartHomeValidationResult> UpdateAsync(PermissionModel model);
    }
}