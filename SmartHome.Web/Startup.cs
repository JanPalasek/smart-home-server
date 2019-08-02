﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SmartHome.Database;
using SmartHome.Database.Repositories;
using SmartHome.Repositories;
using SmartHome.Repositories.Interfaces;
using SmartHome.Web.Filters;

namespace SmartHome.Web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

    public class Startup
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;

        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // db context
            if (hostingEnvironment.IsDevelopment())
            {
                services.AddDbContext<SmartHomeDbContext>(
                    options => options
                        .UseMySql(configuration.GetConnectionString("SmartHomeDatabase"), a => a.MigrationsAssembly("SmartHome.Database"))
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                        // log data to know where is the mistake
                        .EnableSensitiveDataLogging());
            }
            else
            {
                services.AddDbContext<SmartHomeDbContext>(
                    options => options
                        .UseMySql(configuration.GetConnectionString("SmartHomeDatabase"), a => a.MigrationsAssembly("SmartHome.Database")));
            }
            
            #region AutoMapper
            
            var config = new MapperConfiguration(cfg =>
            {
                // get all non-tests assemblies
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.FullName.StartsWith("SmartHome") && !a.FullName.EndsWith("Tests"));
                
                cfg.AddMaps(assemblies);
            });
            config.AssertConfigurationIsValid();
            
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            
            #endregion

            services.AddScoped<SmartHomeAppDbContext>();
            
            #region Repositories

            services.AddScoped<ITemperatureMeasurementRepository, TemperatureMeasurementRepository>();
            services.AddScoped<IBatteryMeasurementRepository, BatteryMeasurementRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();

            #endregion

            #region Filters

            services.AddScoped<TransactionFilter>();

            #endregion

            // add localization to mvc
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            services.AddMvc(options =>
            {
                var stringLocalizerFactory = services
                    .BuildServiceProvider()
                    .GetService<IStringLocalizerFactory>();

                var localizer =
                    stringLocalizerFactory.Create(
                        "ModelBindingMessages", "SmartHome.Web");

                // localize binding errors
                options.ModelBindingMessageProvider
                    .SetAttemptedValueIsInvalidAccessor((x, y) =>
                    localizer["The value '{0}' is not valid for {1}.", x, y]);
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
                    (x) => localizer["The field {0} must be a number."]);
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(
                    (x) => localizer["A value for the '{0}' property was not provided.", x]);
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
                    (x, y) => localizer["The value '{0}' is not valid for {1}.", x, y]);
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
                    () => localizer["A value is required."]);
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(
                    (x) => localizer["The supplied value is invalid for {0}.", x]);
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    (x) => localizer["Null value is invalid.", x]);
            })
                .AddViewLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();

            // set up route mapping
            app.UseMvc(routes =>
            {
                routes.MapRoute("default",
                    "{controller=Home}/{action=Overview}");
            });
        }
    }
}