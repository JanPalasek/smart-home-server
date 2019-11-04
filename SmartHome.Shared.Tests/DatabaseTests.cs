using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SmartHome.Database;
using SmartHome.Database.Entities;

namespace SmartHome.Shared.Tests
{
    [TestFixture]
    public abstract class DatabaseTests : TestsBase
    {
        /// <summary>
        /// Should be used only if we test <see cref="SmartHomeDbContext"/>.
        /// </summary>
        protected SmartHomeDbContext DbContext { get; private set; }

        [SetUp]
        public void SetUpDatabase()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            configurationBuilder.AddJsonFile("appsettings.development.json");

            var configuration = configurationBuilder.Build();

            var builder = new DbContextOptionsBuilder<SmartHomeDbContext>()
                .UseMySql(configuration.GetConnectionString("SmartHomeDatabase"), a => a.MigrationsAssembly("SmartHome.Database"));

            DbContext = new SmartHomeDbContext(builder.Options);
        }
        
        protected TType GetAny<TType>() where TType : class, IId<long>
        {
            return DbContext.Set<TType>().First();
        }
    }
}