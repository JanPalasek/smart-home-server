using System.Threading.Tasks;

namespace SmartHome.DomainCore.ServiceInterfaces.Permission
{
    public interface IPermissionVerificationService
    {
        Task<bool> HasPermissionAsync(string userName, string permissionName);
        bool HasPermission(string userName, string permissionName);
    }
}