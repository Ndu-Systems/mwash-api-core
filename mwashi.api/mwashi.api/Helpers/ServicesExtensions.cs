using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mwashi.api.Contracts;
using mwashi.api.Data;
using mwashi.api.Services;

namespace mwashi.api.Helpers
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

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var appSettingsSection = config.GetSection("ConnectionStrings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var connectionString = appSettings.ProdDatabase;
            if (connectionString != null) services.AddDbContext<RepositoryDbContext>(o => o.UseMySql(connectionString));
        }

        public static void ConfigureAPIServices(this IServiceCollection services)
        {
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }
    }
}
