using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.Shared;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(UserModel model, string password);
        Task<SignInResult> SignInAsync(LoginModel model);
    }
}