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
    public class SmartHomeDbContext : IdentityDbContext<User, IdentityRole<long>, long>
    {

        public SmartHomeDbContext(
            DbContextOptions<SmartHomeDbContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add model building
            modelBuilder.Entity<BatteryPowerSourceType>(builder => { builder.Property(x => x.Id).ValueGeneratedOnAdd(); });

            modelBuilder.Entity<Place>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name).IsRequired();
            });

            modelBuilder.Entity<SensorType>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name).IsRequired();
                
                builder.HasIndex(x => x.Name).IsUnique();
            });
            modelBuilder.Entity<Sensor>(builder => { builder.Property(x => x.Id).ValueGeneratedOnAdd(); });

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
            
            base.OnModelCreating(modelBuilder);
        }
    }
}