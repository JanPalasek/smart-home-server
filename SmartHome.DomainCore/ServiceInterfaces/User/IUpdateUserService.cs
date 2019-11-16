using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.User
{
    public interface IUpdateUserService
    {
        Task<IdentityResult> UpdateUserAsync(UserModel model);
        Task<IdentityResult> AddToOrRemoveFromRolesAsync(long userId, IEnumerable<long> roleIds);
    }
}