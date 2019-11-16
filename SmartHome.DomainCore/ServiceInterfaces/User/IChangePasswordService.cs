using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.ServiceInterfaces.User
{
    public interface IChangePasswordService
    {
        Task<SmartHomeValidationResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel);
    }
}