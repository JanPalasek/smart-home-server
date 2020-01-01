using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.Infrastructure.Tests.Mocks;
using SmartHome.Shared.Tests;

namespace SmartHome.Infrastructure.Tests
{
    [TestFixture]
    public class UserRepositoryTests : DatabaseTests
    {
        private UserRepository repository;
        private User user;
        
        [SetUp]
        public async Task SetUp()
        {
            user = new User()
            {
                Email = "repositoryTestUser@janpalasek.com",
                UserName = "repositoryTestUser",
                NormalizedEmail = "repositoryTestUser@janpalasek.com",
                NormalizedUserName = "repositoryTestUser",
                PasswordHash = "asdfghjkl",
            };
            DbContext.Add(user);

            await DbContext.SaveChangesAsync();
            
            var userManagerMock = new Mock<FakeUserManager>();

            userManagerMock.Setup(x => x.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync((User user, List<string> roles) =>
                {
                    foreach (var role in roles)
                    {
                        long roleId = DbContext.Set<Role>().First(x => x.Name == role).Id;
                        DbContext.Add(new IdentityUserRole<long>()
                        {
                            RoleId = roleId,
                            UserId = user.Id
                        });
                    }

                    DbContext.SaveChanges();

                    return IdentityResult.Success;
                });
            
            repository = new UserRepository(new SmartHomeAppDbContext(DbContext), Mapper,
                userManagerMock.Object, null);
        }

        [Test]
        public async Task AddToRolesAsyncTest()
        {
            var role = await GetAnyAsync<Role>();
            
            await repository.AddToRolesAsync(user.Id, new List<long>(){ role.Id });
            
            Assert.That(DbContext.Set<IdentityUserRole<long>>().Any(x => x.RoleId == role.Id
                                                                         && x.UserId == user.Id), Is.True);
        }
    }
}