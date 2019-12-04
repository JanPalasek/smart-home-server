using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SmartHome.DomainCore;

namespace SmartHome.Database.Entities
{
    public class Role : IdentityRole<long>, IId<long>
    {
    }
}