using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SmartHome.Database;
using SmartHome.Database.Entities;
using SmartHome.DomainCore;

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
            configurationBuilder.AddJsonFile("appsettings.testing.json");

            var configuration = configurationBuilder.Build();

            var builder = new DbContextOptionsBuilder<SmartHomeDbContext>()
                .UseInMemoryDatabase(configuration["Database:Name"]);

            DbContext = new SmartHomeDbContext(builder.Options);
            
            CreateInitialData(DbContext);
        }

        private void CreateInitialData(SmartHomeDbContext context)
        {
            context.Add(new User()
            {
                Email = "admin@janpalasek.com",
                UserName = "admin",
                NormalizedEmail = "admin@janpalasek.com",
                NormalizedUserName = "admin",
                PasswordHash = "asdfghjkl",
            });

            context.Add(new Place()
            {
                Name = "Bathroom",
                Note = "Bathroom note"
            });
            context.Add(new Place()
            {
                Name = "Living room"
            });

            context.Add(new Role()
            {
                Name = "Admin",
                NormalizedName = "Admin"
            });

            context.SaveChanges();
        }
        
        protected TType GetAny<TType>() where TType : class, IId<long>
        {
            return DbContext.Set<TType>().First();
        }
        
        protected Task<TType> GetAnyAsync<TType>() where TType : class, IId<long>
        {
            return DbContext.Set<TType>().FirstAsync();
        }
    }
}