using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.Shared;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddUser(CreateUserModel model);
        Task<SignInResult> SignInAsync(LoginModel model);

        Task<UserModel> GetUserAsync(string email);

        Task<UserModel> GetUserAsync(long id);

        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel);
    }
}