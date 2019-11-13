using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.Admin
{
    public interface IGetUsersService
    {
        Task<UserModel> GetByIdAsync(long id);
        Task<UserModel?> GetByEmailAsync(string email);
        Task<IList<UserModel>> GetAllUsersAsync();
    }
}