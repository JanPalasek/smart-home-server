using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SmartHome.Database.Entities;
using SmartHome.Shared.Tests;

namespace SmartHome.Infrastructure.Tests
{
    [TestFixture]
    public class SmartHomeAppDbContextTests : DatabaseTests
    {
        private SmartHomeAppDbContext appDbContext;
        
        [SetUp]
        public void SetUp()
        {
            appDbContext = new SmartHomeAppDbContext(DbContext);
        }

        [Test]
        [TestCase("Measurement.Temperature.View", true)]
        [TestCase("Measurement.Invalid.View", false)]
        public async Task AddRangeAsyncTest(string newPermissionName, bool alreadyExists)
        {
            var invalidAdd = new Permission()
            {
                Name = newPermissionName
            };

            Assert.That(await DbContext.Set<Permission>().AnyAsync(x => x.Name == newPermissionName),
                Is.EqualTo(alreadyExists));
            await appDbContext.AddRangeAsync(new List<Permission>() {invalidAdd});
            Assert.That(await DbContext.Set<Permission>().AnyAsync(x => x.Name == newPermissionName),
                Is.EqualTo(true));
        }
    }
}