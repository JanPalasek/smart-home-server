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

        public Task<SignInResult> SignInAsync(LoginModel model)
        {
            return SignInAsync(model.Email, model.Password);
        }

        public async Task<SignInResult> SignInAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            await signInManager.SignOutAsync();
            var result = await signInManager.PasswordSignInAsync(user, password, false, false);
            
            return result;
        }
    }
}