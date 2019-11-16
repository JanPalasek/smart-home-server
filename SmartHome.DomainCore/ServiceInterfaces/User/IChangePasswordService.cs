using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.User
{
    public interface IChangePasswordService
    {
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel);
    }
}