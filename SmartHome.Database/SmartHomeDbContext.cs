using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SmartHome.Database.Entities;

namespace SmartHome.Database
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Represents BazaarFilter database.
    /// </summary>
    public class SmartHomeDbContext : IdentityDbContext<User, Role, long>
    {

        public SmartHomeDbContext(
            DbContextOptions<SmartHomeDbContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add model building
            modelBuilder.Entity<BatteryPowerSourceType>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name).IsRequired();
            });

            modelBuilder.Entity<Place>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name).IsRequired();

                builder.HasIndex(x => x.Name).IsUnique();
            });

            modelBuilder.Entity<SensorType>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name).IsRequired();
                
                builder.HasIndex(x => x.Name).IsUnique();
            });
            modelBuilder.Entity<Sensor>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name).IsRequired();

                builder.HasIndex(x => x.PlaceId);
                builder.HasIndex(x => x.SensorTypeId);
                builder.HasIndex(x => x.BatteryPowerSourceTypeId);
            });

            #region Measurements
            
            modelBuilder.Entity<TemperatureMeasurement>(builder =>
            {
                builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                builder.HasIndex(x => x.MeasurementDateTime);
                builder.HasIndex(x => x.SensorId);
            });
            
            modelBuilder.Entity<HumidityMeasurement>(builder =>
            {
                builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                builder.HasIndex(x => x.MeasurementDateTime);
                builder.HasIndex(x => x.SensorId);
            });
            
            modelBuilder.Entity<BatteryMeasurement>(builder =>
            {
                builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                builder.HasIndex(x => x.MeasurementDateTime);
                builder.HasIndex(x => x.SensorId);
            });
            
            #endregion

            modelBuilder.Entity<Permission>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name).IsRequired();

                builder.HasIndex(x => x.Name).IsUnique();
            });

            modelBuilder.Entity<UserPermission>(builder =>
            {
                builder.Property(x => x.Id);
                
                builder.HasIndex(x => new {x.PermissionId, x.UserId}).IsUnique();
            });
            
            modelBuilder.Entity<RolePermission>(builder =>
            {
                builder.Property(x => x.Id);
                
                builder.HasIndex(x => new {x.PermissionId, x.RoleId}).IsUnique();
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}