using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Admin;

namespace SmartHome.Services.Admin
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IUserRepository repository;

        public GetUsersService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UserModel> GetByIdAsync(long id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<UserModel?> GetByEmailAsync(string email)
        {
            return await repository.GetUserByEmailAsync(email);
        }

        public Task<IList<UserModel>> GetAllUsersAsync()
        {
            return repository.GetAllAsync();
        }

        public async Task<UserModel?> GetByNameAsync(string name)
        {
            return await repository.GetUserByNameAsync(name);
        }
    }
}