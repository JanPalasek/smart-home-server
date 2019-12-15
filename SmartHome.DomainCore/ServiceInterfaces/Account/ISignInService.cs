using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Account
{
    public interface ISignInService
    {
        Task<ServiceResult<long>> SignInAsync(LoginModel model);
    }
}