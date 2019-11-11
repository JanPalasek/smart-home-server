using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Admin;

namespace SmartHome.Services.Admin
{
    public class CreateUserService : ICreateUserService
    {
        private readonly IUserRepository repository;

        public CreateUserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserModel model)
        {
            var identityErrors = await ValidateAsync(model);
            
            if (identityErrors.Count > 0)
            {
                return IdentityResult.Failed(identityErrors.ToArray());
            }
            
            return await repository.CreateUserAsync(model);
        }

        private async Task<IList<IdentityError>> ValidateAsync(CreateUserModel model)
        {
            var identityErrors = new List<IdentityError>();
            if (await repository.GetUserByNameAsync(model.UserName) != null)
            {
                identityErrors.Add(new IdentityError()
                {
                    Code = nameof(model.UserName),
                    Description = $"User with name {model.UserName} already exists."
                });
            }
            
            if (await repository.GetUserByNameAsync(model.UserName) != null)
            {
                identityErrors.Add(new IdentityError()
                {
                    Code = nameof(model.Email),
                    Description = $"User with email {model.Email} already exists."
                });
            }

            return identityErrors;
        }
    }
}