using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.User;

namespace SmartHome.Services.User
{
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly IUserRepository repository;

        public ChangePasswordService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var errors = await ValidateAsync(changePasswordModel);
            if (errors.Count > 0)
            {
                return IdentityResult.Failed(errors.ToArray());
            }
            
            return await repository.ChangePasswordAsync(changePasswordModel);
        }

        private async Task<IList<IdentityError>> ValidateAsync(ChangePasswordModel model)
        {
            var errors = new List<IdentityError>();

            if (string.Equals(model.OldPassword, model.NewPassword))
            {
                errors.Add(new IdentityError()
                {
                    Code = nameof(model.NewPassword),
                    Description = "Old password cannot be same as the new password."
                });
            }

            return errors;
        }
    }
}