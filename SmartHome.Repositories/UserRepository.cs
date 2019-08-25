using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SmartHome.Common;
using SmartHome.Common.Extensions;
using SmartHome.Database.Entities;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
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

        public async Task<IdentityResult> AddUser(CreateUserModel model)
        {
            var entity = Mapper.Map<User>(model);

            if (entity.Id > 0)
            {
                return await userManager.UpdateAsync(entity);
            }
            
            return await userManager.CreateAsync(entity, model.Password);

        }

        public async Task<SignInResult> SignInAsync(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Login);

            if (user == null && !model.Login.IsEmail())
            {
                return null;
            }
            
            if (user == null && model.Login.IsEmail())
            {
                user = await userManager.FindByEmailAsync(model.Login);
            }

            if (user == null)
            {
                // couldn't sign in with login or email
                return null;
            }

            await signInManager.SignOutAsync();
            var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            
            return result;
        }

        public async Task<UserModel> GetUserAsync(string email)
        {
            var entity = await userManager.FindByEmailAsync(email);
            return Mapper.Map<UserModel>(entity);
        }
        
        public async Task<UserModel> GetUserAsync(long id)
        {
            var entity = await SmartHomeAppDbContext.SingleAsync<User>(id);
            return Mapper.Map<UserModel>(entity);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var user = await SmartHomeAppDbContext.SingleAsync<User>(changePasswordModel.Id);

            return await userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
        }
    }
}