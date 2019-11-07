using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore;

namespace SmartHome.Database.Entities
{
    public class User : IdentityUser<long>, IId<long>
    {
    }
}