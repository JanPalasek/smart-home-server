using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.User;

namespace SmartHome.Services.User
{
    public class CreateUserService : ICreateUserService
    {
        private readonly IUserRepository repository;

        public CreateUserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SmartHomeValidationResult> CreateUserAsync(CreateUserModel model)
        {
            var validationResult = await ValidateAsync(model);
            
            if (!validationResult.Succeeded)
            {
                return validationResult;
            }
            
            return SmartHomeValidationResult.FromIdentityResult(await repository.CreateUserAsync(model));
        }

        private async Task<SmartHomeValidationResult> ValidateAsync(CreateUserModel model)
        {
            var identityErrors = new List<SmartHomeValidation>();
            if (await repository.GetUserByNameAsync(model.UserName!) != null)
            {
                identityErrors.Add(new SmartHomeValidation(nameof(model.UserName),
                    $"User with name {model.UserName} already exists."));
            }
            
            if (await repository.GetUserByEmailAsync(model.Email!) != null)
            {
                identityErrors.Add(new SmartHomeValidation(nameof(model.Email),
                    $"User with email {model.Email} already exists."));
            }

            return SmartHomeValidationResult.Failed(identityErrors);
        }
    }
}