using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using SmartHome.Database.Entities;

namespace SmartHome.Infrastructure.Tests.Mocks
{
    public class FakeRoleManager : RoleManager<Role>
    {
        public FakeRoleManager() : base(
            new Mock<IRoleStore<Role>>().Object, new Mock<IEnumerable<IRoleValidator<Role>>>().Object,
            new Mock<ILookupNormalizer>().Object, new Mock<IdentityErrorDescriber>().Object,
            new Mock<ILogger<RoleManager<Role>>>().Object
            )
        {
        }
    }
}