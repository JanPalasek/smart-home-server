using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartHome.DomainCore.ServiceInterfaces.User
{
    public interface IDeleteUserService
    {
        Task<IdentityResult> DeleteUserAsync(long id);
    }
}