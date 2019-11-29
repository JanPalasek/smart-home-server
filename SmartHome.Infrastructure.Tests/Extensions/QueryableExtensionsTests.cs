using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SmartHome.Database.Entities;
using SmartHome.DomainCore.Data;
using SmartHome.Infrastructure.Extensions;
using SmartHome.Shared.Tests;

namespace SmartHome.Infrastructure.Tests.Extensions
{
    [TestFixture]
    public class QueryableExtensionsTests : DatabaseTests
    {
        [Test]
        public void PageByTest()
        {
            var query = DbContext.Set<Place>().AsQueryable();

            var item1 = query.PageBy(new PagingArgs(0, 1)).ToList().Single();
            var item2 = query.PageBy(new PagingArgs(1, 1)).ToList().Single();
            
            Assert.That(item1.Id, Is.Not.EqualTo(item2.Id));
            
        }
    }
}