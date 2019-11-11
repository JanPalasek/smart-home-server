using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Account
{
    public interface ISignInService
    {
        Task<SignInResult> SignInAsync(LoginModel model);
    }
}