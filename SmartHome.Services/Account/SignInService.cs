using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.Common.Extensions;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Account;

namespace SmartHome.Services.Account
{
    public class SignInService : ISignInService
    {
        private readonly IUserRepository repository;

        public SignInService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SignInResult> SignInAsync(LoginModel model)
        {
            var user = await repository.GetUserByNameAsync(model.Login);

            if (user == null && !model.Login.IsEmail())
            {
                return SignInResult.Failed;
            }
            
            if (user == null && model.Login.IsEmail())
            {
                user = await repository.GetUserByEmailAsync(model.Login);
            }

            if (user == null)
            {
                return SignInResult.Failed;
            }

            await repository.SignOutAsync();
            var result = await repository.SignInAsync(user, model.Password, model.RememberMe);
            
            return result;
        }
    }
}