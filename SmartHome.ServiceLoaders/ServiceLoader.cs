using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Database.Repositories;
using SmartHome.Repositories;
using SmartHome.Repositories.Interfaces;

namespace SmartHome.ServiceLoaders
{
    public abstract class ServiceLoader
    {
        public abstract IServiceCollection Load(IServiceCollection services);
        
        protected IServiceCollection LoadAutoMapper(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                // get all non-tests assemblies
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.FullName.StartsWith("SmartHome"));
                
                cfg.AddMaps(assemblies);
            });
            config.AssertConfigurationIsValid();
            
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        protected IServiceCollection LoadRepositories(IServiceCollection services)
        {
            services.AddScoped<SmartHomeAppDbContext>();
            
            services.AddScoped<ITemperatureMeasurementRepository, TemperatureMeasurementRepository>();
            services.AddScoped<IBatteryMeasurementRepository, BatteryMeasurementRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IBatteryPowerSourceTypeRepository, BatteryPowerSourceTypeRepository>();
            services.AddScoped<IUnitTypeRepository, UnitTypeRepository>();

            return services;
        }
    }
}