using System.Threading.Tasks;

namespace SmartHome.DomainCore.ServiceInterfaces.Permission
{
    public interface IDeletePermissionService
    {
        Task DeleteAsync(long id);
    }
}