using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using SmartHome.Database;
using SmartHome.Database.Entities;

namespace SmartHome.Infrastructure.Tests.Mocks
{
    public class FakeUserManagerMock : Mock<FakeUserManager>
    {
        private readonly SmartHomeDbContext context;

        public FakeUserManagerMock(SmartHomeDbContext context, bool allSetupDefault)
        {
            this.context = context;

            if (allSetupDefault)
            {
                SetupAddToRoles();
                SetupGetRolesAsync();
            }
        }

        public void SetupAddToRoles()
        {
            Setup(x => x.AddToRolesAsync(
                    It.IsAny<User>(), It.IsAny<IEnumerable<string>>()))
                .Returns(async (User user, IEnumerable<string> roles) =>
                {
                    foreach (var role in roles)
                    {
                        long roleId = (await context.Set<Role>().FirstAsync(x => x.Name == role)).Id;
                        context.Add(new IdentityUserRole<long>()
                        {
                            RoleId = roleId,
                            UserId = user.Id
                        });
                    }

                    await context.SaveChangesAsync();

                    return IdentityResult.Success;
                });
        }

        public void SetupGetRolesAsync()
        {
            Setup(x => x.GetRolesAsync(It.IsAny<User>()))
                .Returns(async (User user) =>
                {
                    var roleIds = await context.Set<IdentityUserRole<long>>()
                        .Where(x => x.UserId == user.Id).Select(x => x.RoleId)
                        .ToListAsync();

                    var roles = await context.Set<Role>()
                        .Where(x => roleIds.Contains(x.Id))
                        .Select(x => x.Name)
                        .ToListAsync();
                    return roles;
                });
        }
    }
}