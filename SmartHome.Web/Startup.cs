﻿using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartHome.DomainCore.ServiceInterfaces.Permission;
using SmartHome.ServiceLoaders;
using SmartHome.Web.Configurations;
using SmartHome.Web.Utils;

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

    public class Startup
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;

        public Startup(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            new WebLoader(configuration, hostingEnvironment.IsDevelopment()).Load(services);
            
            // generate key
            int keyBytesLength = configuration.GetSection("Jwt:KeyBytesLength").Get<int>();
            var keyBytes = new byte[keyBytesLength];
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetBytes(keyBytes);

            services.AddScoped(provider =>
            {
                var configurationProvider = provider.GetRequiredService<IConfiguration>();
                var parsedConfiguration = configurationProvider.GetSection("Jwt").Get<JwtConfiguration>();
                parsedConfiguration.SecurityKey = new SymmetricSecurityKey(keyBytes);
                
                // TODO: verify

                return parsedConfiguration;
            });
            services.AddScoped(provider =>
            {
                var configurationProvider = provider.GetRequiredService<IConfiguration>();
                var parsedConfiguration = configurationProvider.GetSection("FileManager").Get<FileManagerConfiguration>();
                
                // TODO: verify it has all properties not null and not empty
                  
                return parsedConfiguration;
            });
            
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
    
                    var provider = services
                        .BuildServiceProvider();
                    var configuration = provider.GetRequiredService<JwtConfiguration>();
                    
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = configuration.Issuer,
                        ValidAudience = configuration.Audience,
                        IssuerSigningKey = configuration.SecurityKey
                    };
                });
            
            services.AddAuthorization(options =>
            {
                var provider = services
                    .BuildServiceProvider();
                var permissions = provider
                    .GetService<IGetPermissionsService>().GetAllPermissionsAsync().Result;
                foreach(var permission in permissions) 
                {
                    options.AddPolicy(permission.Name!,
                        policy => policy.Requirements.Add(new PermissionRequirement(permission.Name!)));
                }
            });
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            
            // set up MVC
            var mvcConfiguration = services.AddMvc(options =>
            {
                var provider = services
                    .BuildServiceProvider();
                var stringLocalizerFactory = provider
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

                options.EnableEndpointRouting = false;
            }).AddViewLocalization();
            
            if (hostingEnvironment.IsDevelopment())
            {
                mvcConfiguration.AddRazorRuntimeCompilation();
            }

            services.AddSwaggerGen(options =>
            {
                var provider = services
                    .BuildServiceProvider();
                var configurationProvider = provider.GetRequiredService<IConfiguration>();
                var parsedConfiguration = configurationProvider.GetSection("Swagger").Get<SwaggerConfiguration>();
                
                options.SwaggerDoc(parsedConfiguration.Name, new OpenApiInfo()
                {
                    Title = parsedConfiguration.Title,
                    Version = parsedConfiguration.Version
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization using bearer scheme. E.g.: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            BearerFormat = "JWT"
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            string? licenseKey = configuration.GetSection("SyncfusionLicenseKey").Get<string?>();
            if (licenseKey != null)
            {
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            var swaggerConfiguration = configuration.GetSection("Swagger").Get<SwaggerConfiguration>();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerConfiguration.EndpointUrl, swaggerConfiguration.Name);
                options.RoutePrefix = swaggerConfiguration.RoutePrefix;
            });
            
            // set up route mapping
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Overview}/{id:int?}");
            });
        }
    }
}