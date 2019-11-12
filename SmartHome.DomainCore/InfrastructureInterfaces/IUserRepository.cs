using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IUserRepository : IGetByIdRepository<UserModel>
    {
        Task<IdentityResult> CreateUserAsync(CreateUserModel model);

        Task<IdentityResult> UpdateUserAsync(CreateUserModel model);
        Task<SignInResult> SignInAsync(UserModel model, string password, bool rememberMe);
        
        Task SignOutAsync();

        Task<UserModel?> GetUserByEmailAsync(string email);

        Task<UserModel?> GetUserByNameAsync(string name);

        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel);

        Task<IdentityResult> AddRoleAsync(long userId, string roleName);
    }
}