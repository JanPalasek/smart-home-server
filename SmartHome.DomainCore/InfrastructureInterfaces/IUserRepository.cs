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

        Task<IdentityResult> DeleteUserAsync(long id);
        Task<SignInResult> SignInAsync(UserModel model, string password, bool rememberMe);
        
        Task SignOutAsync();
        
        /// <summary>
        /// Use this preferably to sign out other users.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task SignOutAsync(long userId);

        Task<UserModel?> GetUserByEmailAsync(string email);

        Task<UserModel?> GetUserByNameAsync(string name);

        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel);

        Task<IdentityResult> AddToRoleAsync(long userId, long roleId);

        Task<IdentityResult> RemoveFromRoleAsync(long userId, long roleId);

        Task AddPermissionsToUserAsync(long userId, List<int> permissionIds);

        Task RemovePermissionsFromUserAsync(long userId, List<int> permissionIds);

        Task<IdentityResult> AddToRolesAsync(long userId, List<long> roleIds);
        Task<IdentityResult> RemoveFromRolesAsync(long userId, List<long> roleIds);
    }
}