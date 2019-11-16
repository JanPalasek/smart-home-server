using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.User
{
    public interface ICreateUserService
    {
        Task<IdentityResult> CreateUserAsync(CreateUserModel model);
    }
}