using Microsoft.AspNetCore.Identity;
using SmartHome.Shared;

namespace SmartHome.Database.Entities
{
    public class User : IdentityUser<long>, IId<long>
    {
    }
}