using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.User
{
    public interface IGetUsersService
    {
        Task<UserModel> GetByIdAsync(long id);
        Task<UserModel?> GetByEmailAsync(string email);
        Task<IList<UserModel>> GetAllUsersAsync();
        Task<UserModel?> GetByNameAsync(string name);
    }
}