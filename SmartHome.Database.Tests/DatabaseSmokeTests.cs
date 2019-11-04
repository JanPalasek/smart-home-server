using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SmartHome.Database.Entities;
using SmartHome.Shared.Tests;

namespace SmartHome.Database.Tests
{
    [TestFixture]
    public class DatabaseSmokeTests : DatabaseTests
    {
        [Test(Description = "Tests that it is possible to connect to database by obtaining a simple entry that has to be there.")]
        public void TestConnection()
        {
            var user = GetAny<User>();
            
            Assert.That(user, Is.Not.Null);
        }
    }
}