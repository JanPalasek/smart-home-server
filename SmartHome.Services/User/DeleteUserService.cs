using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.User;

namespace SmartHome.Services.User
{
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IUserRepository repository;

        public DeleteUserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IdentityResult> DeleteUserAsync(long id)
        {
            return await repository.DeleteUserAsync(id);
        }
    }
}