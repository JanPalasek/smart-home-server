using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Validations;

namespace SmartHome.DomainCore.ServiceInterfaces.User
{
    public interface IDeleteUserService
    {
        Task<SmartHomeValidationResult> DeleteUserAsync(long id);
    }
}