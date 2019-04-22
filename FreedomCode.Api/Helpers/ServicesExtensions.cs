using FreedomCode.Api.Contracts;
using FreedomCode.Api.Data;
using FreedomCode.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreedomCode.Api.Helpers
{
    public static class ServicesExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials());
            });
        }

        public static void ConfigureSQLServer(this IServiceCollection services, IConfiguration config)
        {
            //Configure strongly typed appSetting.json objects
            var appSettingsSection = config.GetSection("ConnectionStrings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var connectionString = appSettings.FreedomCodeDatabase;
            if (connectionString != null) services.AddDbContext<FreedomCodeContext>(o => o.UseSqlServer(connectionString));

        }

        public static void ConfigureAPIServices(this IServiceCollection services)
        {
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        public static void ConfigureJWTAuthentication(this IServiceCollection services, IConfiguration config)
        {
            //Configure strongly typed appSetting.json objects
            var appSettingsSection = config.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }


    }
}
