using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SmartHome.Common;
using SmartHome.Common.DateTimeProviders;
using SmartHome.Database;
using SmartHome.Database.Entities;
using SmartHome.DomainCore;
using SmartHome.DomainCore.Data;

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
        public async Task SetUpDatabase()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.testing.json");

            var configuration = configurationBuilder.Build();

            var builder = new DbContextOptionsBuilder<SmartHomeDbContext>()
                .UseInMemoryDatabase(configuration["Database:Name"]);

            DbContext = new SmartHomeDbContext(builder.Options);
            
            await CreateInitialData(DbContext, DateTimeProvider);
        }
        
        [TearDown]
        public async Task TearDownDatabase()
        {
            await DbContext.Database.EnsureDeletedAsync();
        }

        private async Task CreateInitialData(SmartHomeDbContext context, IDateTimeProvider dateTimeProvider)
        {
            var adminUser = new User()
            {
                Email = "admin@janpalasek.com",
                UserName = "admin",
                NormalizedEmail = "admin@janpalasek.com",
                NormalizedUserName = "admin",
                PasswordHash = "asdfghjkl",
            };
            var user = new User()
            {
                Email = "user@janpalasek.com",
                UserName = "user",
                NormalizedEmail = "user@janpalasek.com",
                NormalizedUserName = "user",
                PasswordHash = "asdfghjkl",
            };
            context.Add(adminUser);
            context.Add(user);

            var fileView = new Permission()
            {
                Name = "File.View"
            };
            var fileEdit = new Permission()
            {
                Name = "File.Edit"
            };
            var measurementTemperatureView = new Permission()
            {
                Name = "Measurement.Temperature.View"
            };
            var measurementTemperatureEdit = new Permission()
            {
                Name = "Measurement.Temperature.Edit"
            };
            context.Add(fileView);
            context.Add(fileEdit);
            context.Add(measurementTemperatureView);
            context.Add(measurementTemperatureEdit);
            
            var bathroom = new Place()
            {
                Name = "Bathroom",
                Note = "Bathroom note"
            };
            var livingRoom = new Place()
            {
                Name = "Living room"
            };
            context.Add(bathroom);
            context.Add(livingRoom);
            
            var adminRole = new Role()
            {
                Name = "Admin",
                NormalizedName = "Admin"
            };
            var userRole = new Role()
            {
                Name = "User",
                NormalizedName = "User"
            };
            context.Add(adminRole);
            context.Add(userRole);
            
            context.Add(new IdentityUserRole<long>()
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id
            });
            context.Add(new IdentityUserRole<long>()
            {
                RoleId = userRole.Id,
                UserId = user.Id
            });
            context.Add(new RolePermission()
            {
                PermissionId = fileView.Id,
                RoleId = adminRole.Id
            });
            context.Add(new RolePermission()
            {
                PermissionId = fileEdit.Id,
                RoleId = adminRole.Id
            });
            context.Add(new RolePermission()
            {
                PermissionId = measurementTemperatureView.Id,
                RoleId = adminRole.Id
            });
            context.Add(new RolePermission()
            {
                PermissionId = measurementTemperatureEdit.Id,
                RoleId = adminRole.Id
            });
            context.Add(new RolePermission()
            {
                PermissionId = measurementTemperatureView.Id,
                RoleId = userRole.Id
            });
            context.Add(new RolePermission()
            {
                PermissionId = fileView.Id,
                RoleId = userRole.Id
            });

            context.Add(new UserPermission()
            {
                PermissionId = measurementTemperatureEdit.Id,
                UserId = user.Id
            });
            
            // enumerations
            var dht11SensorType = new SensorType()
            {
                Name = "DHT11",
                Description = "Humidity and temperature sensor."
            };
            context.Add(dht11SensorType);
            var alkalineBatteryPowerSourceType = new BatteryPowerSourceType()
            {
                BatteryType = BatteryType.Alkaline,
                MaximumVoltage = 4.5,
                MinimumVoltage = 3.3,
                Name = "Alkaline 3x1.5"
            };
            context.Add(alkalineBatteryPowerSourceType);
            var sensor = new Sensor()
            {
                BatteryPowerSourceTypeId = alkalineBatteryPowerSourceType.Id,
                MinimumRequiredVoltage = 3,
                PlaceId = bathroom.Id,
                SensorTypeId = dht11SensorType.Id,
                Name = "DHT11 sensor"
            };
            context.Add(sensor);
            
            var measurement = new TemperatureMeasurement()
            {
                MeasurementDateTime = dateTimeProvider.Now.AddYears(-3),
                PlaceId = bathroom.Id,
                SensorId = sensor.Id,
                Temperature = 30
            };
            context.Add(measurement);
            measurement = new TemperatureMeasurement()
            {
                MeasurementDateTime = dateTimeProvider.Now.AddYears(-2),
                PlaceId = livingRoom.Id,
                SensorId = sensor.Id,
                Temperature = 24
            };
            context.Add(measurement);

            await context.SaveChangesAsync();
        }
        
        protected Task<TType> GetAnyAsync<TType>() where TType : class, IId<long>
        {
            return DbContext.Set<TType>().FirstAsync();
        }
    }
}