using System.Threading.Tasks;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Account;

namespace SmartHome.Services.Account
{
    public class SignOutService : ISignOutService
    {
        private readonly IUserRepository repository;

        public SignOutService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public Task SignOutAsync()
        {
            return repository.SignOutAsync();
        }
    }
}