using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Permission
{
    public interface ICreatePermissionService
    {
        Task<ServiceResult<long>> CreateAsync(PermissionModel model);
    }
}