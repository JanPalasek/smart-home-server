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
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly IUserRepository repository;

        public ChangePasswordService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SmartHomeValidationResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var errors = Validate(changePasswordModel);
            if (!errors.Succeeded)
            {
                return errors;
            }
            
            return SmartHomeValidationResult.FromIdentityResult(await repository.ChangePasswordAsync(changePasswordModel));
        }

        private SmartHomeValidationResult Validate(ChangePasswordModel model)
        {
            var errors = new List<SmartHomeValidation>();

            if (string.Equals(model.OldPassword, model.NewPassword))
            {
                errors.Add(new SmartHomeValidation(nameof(model.NewPassword),
                    "Old password cannot be same as the new password."));
            }

            return SmartHomeValidationResult.Failed(errors);
        }
    }
}