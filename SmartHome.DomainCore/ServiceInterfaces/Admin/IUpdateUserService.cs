using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Admin
{
    public interface IUpdateUserService
    {
        Task<IdentityResult> UpdateUserAsync(UserModel model);
    }
}