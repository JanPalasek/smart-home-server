using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        
        public UserRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(smartHomeAppDbContext, mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserModel model)
        {
            var entity = Mapper.Map<User>(model);
            
            return await userManager.CreateAsync(entity, model.Password);

        }

        public async Task<IdentityResult> UpdateUserAsync(CreateUserModel model)
        {
            var entity = Mapper.Map<User>(model);
            return await userManager.UpdateAsync(entity);
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
        
        public async Task<IdentityResult> AddRoleAsync(long userId, string roleName)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(userId);
            return await userManager.AddToRoleAsync(user, roleName);
        }
    }
}