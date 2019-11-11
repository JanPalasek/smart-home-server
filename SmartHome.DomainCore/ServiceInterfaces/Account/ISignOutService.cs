using System.Threading.Tasks;

namespace SmartHome.DomainCore.ServiceInterfaces.Account
{
    public interface ISignOutService
    {
        Task SignOutAsync();
    }
}