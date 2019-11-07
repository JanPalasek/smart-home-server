using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddUser(CreateUserModel model);
        Task<SignInResult?> SignInAsync(LoginModel model);

        Task<UserModel> GetUserAsync(string email);

        Task<UserModel> GetUserAsync(long id);

        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel);
    }
}