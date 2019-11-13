using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore.Data.Models;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Admin;

namespace SmartHome.Services.Admin
{
    public class UpdateUserService : IUpdateUserService
    {
        private readonly IUserRepository repository;

        public UpdateUserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IdentityResult> UpdateUserAsync(UserModel model)
        {
            return await repository.UpdateUserAsync(model);
        }
    }
}