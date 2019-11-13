using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.InfrastructureInterfaces
{
    public interface IUserRepository : IGetByIdRepository<UserModel>, IGetAllRepository<UserModel>
    {
        Task<IdentityResult> CreateUserAsync(CreateUserModel model);

        Task<IdentityResult> UpdateUserAsync(UserModel model);
        Task<SignInResult> SignInAsync(UserModel model, string password, bool rememberMe);
        
        Task SignOutAsync();

        Task<UserModel?> GetUserByEmailAsync(string email);

        Task<UserModel?> GetUserByNameAsync(string name);

        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel);

        Task<IdentityResult> AddToRoleAsync(long userId, long roleId);

        Task<IdentityResult> RemoveFromRoleAsync(long userId, long roleId);

        Task<IdentityResult> AddToRolesAsync(long userId, IEnumerable<long> roleIds);
        Task<IdentityResult> RemoveFromRolesAsync(long userId, IEnumerable<long> roleIds);
    }
}