using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHome.Common;
using SmartHome.Common.Extensions;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;

namespace SmartHome.Infrastructure
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        
        public UserRepository(SmartHomeAppDbContext smartHomeAppDbContext,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager) : base(smartHomeAppDbContext, mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserModel model)
        {
            var entity = Mapper.Map<User>(model);
            
            return await userManager.CreateAsync(entity, model.Password);

        }

        public Task<IdentityResult> UpdateUserAsync(UserModel model)
        {
            throw new NotSupportedException();
        }

        public async Task<IdentityResult> DeleteUserAsync(long id)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(id);
            return await userManager.DeleteAsync(user);
        }

        public async Task<SignInResult> SignInAsync(UserModel model, string password, bool rememberMe)
        {
            var user = await SingleAsync(model.Id);
            return await signInManager.PasswordSignInAsync(user, password, rememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task SignOutAsync(long userId)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);
            await userManager.UpdateSecurityStampAsync(user);
        }

        public async Task<UserModel?> GetUserByEmailAsync(string email)
        {
            var entity = await userManager.FindByEmailAsync(email);
            return Mapper.Map<UserModel>(entity);
        }

        public async Task<UserModel?> GetUserByNameAsync(string name)
        {
            var entity = await userManager.FindByNameAsync(name);
            return Mapper.Map<UserModel>(entity);
        }
        
        public async Task<UserModel> GetByIdAsync(long id)
        {
            var entity = await SmartHomeAppDbContext.SingleAsync<User>(id);
            return Mapper.Map<UserModel>(entity);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(changePasswordModel.Id);

            return await userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
        }

        public async Task<IdentityResult> AddToRoleAsync(long userId, long roleId)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);
            var roleName = (await SmartHomeAppDbContext.SingleAsync<Role>(roleId)).Name;
            return await userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> AddToRolesAsync(long userId, List<long> roleIds)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);
            // this query needs for some reason roleIds to be List
            var roleNames = await SmartHomeAppDbContext.Query<Role>().Where(x => roleIds.Contains(x.Id))
                .Select(x => x.Name).ToListAsync();
            
            return await userManager.AddToRolesAsync(user, roleNames);
        }

        public async Task AddPermissionsToUserAsync(long userId, List<string> permissions)
        {
            var permissionIds = await SmartHomeAppDbContext.Query<Permission>()
                .Where(x => permissions.Contains(x.Name))
                .Select(x => x.Id)
                .ToListAsync();

            var entities = permissionIds.Select(x => new UserPermission()
            {
                UserId = userId,
                PermissionId = x
            });

            // get already existing user permissions from the database
            var existingUserPermissions = await SmartHomeAppDbContext.Query<UserPermission>()
                .Where(x => x.UserId == userId)
                .Where(x => permissionIds.Contains(x.PermissionId))
                .ToListAsync();
            
            // insert only those that are not in the database
            entities = entities.Except(existingUserPermissions, EqualityComparerFactory.Create<UserPermission>(
                    x => x.PermissionId.GetHashCode(),
                    (x, y) => x.PermissionId == y.PermissionId && x.UserId == y.UserId));
            
            await SmartHomeAppDbContext.AddRangeAsync(entities);
        }
        
        public async Task RemovePermissionsFromUserAsync(long userId, List<long> permissions)
        {
            var permissionsToRemove = await SmartHomeAppDbContext.Query<UserPermission>()
                .Where(x => x.UserId == userId)
                .Where(x => permissions.Contains(x.Permission.Id))
                .ToListAsync();
            
            await SmartHomeAppDbContext.DeleteRangeAsync(permissionsToRemove);
        }


        public async Task<IdentityResult> RemoveFromRoleAsync(long userId, long roleId)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);
            var roleName = (await SmartHomeAppDbContext.SingleAsync<Role>(roleId)).Name;
            return await userManager.RemoveFromRoleAsync(user, roleName);
        }
        
        public async Task<IdentityResult> RemoveFromRolesAsync(long userId, List<long> roleIds)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);
            var roleNames = await SmartHomeAppDbContext.Query<Role>().Where(x => roleIds.Contains(x.Id))
                .Select(x => x.Name).ToListAsync();

            return await userManager.RemoveFromRolesAsync(user, roleNames);
        }

        public async Task<bool> HasPermissionAsync(long userId, string permissionName)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);
            bool hasPermission = await SmartHomeAppDbContext.Query<UserPermission>()
                .AnyAsync(x => x.UserId == userId && x.Permission.Name == permissionName);

            if (hasPermission)
            {
                return true;
            }
            
            // has role that has that permission
            var roles = (List<string>)await userManager.GetRolesAsync(user);
            hasPermission = await SmartHomeAppDbContext.Query<RolePermission>()
                // filter only for roles that user has
                .Where(x => roles.Contains(x.Role.Name))
                // does the role have that permission?
                .AnyAsync(x => x.Permission.Name == permissionName);

            return hasPermission;

        }

        public async Task<IList<UserModel>> GetAllAsync()
        {
            return await SmartHomeAppDbContext.Query<User>().ProjectTo<UserModel>(Mapper.ConfigurationProvider).ToListAsync();
        }
    }
}