using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SmartHome.Database.Entities;
using SmartHome.Repositories.Interfaces;
using SmartHome.Shared;
using SmartHome.Shared.Models;

namespace SmartHome.Repositories
{
    public class UserRepository : StandardRepository<User, UserModel>, IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        
        public UserRepository(SmartHomeAppDbContext smartHomeAppDbContext, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(smartHomeAppDbContext, mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserModel model, string password)
        {
            var entity = Mapper.Map<User>(model);
            
            var result = await userManager.CreateAsync(entity);
            return result;
        }

        public async Task<SignInResult> SignInAsync(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return null;
            }

            await signInManager.SignOutAsync();
            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            
            return result;
        }
    }
}