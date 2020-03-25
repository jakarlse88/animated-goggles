using Demelain.Server.Data;
using Demelain.Server.Repositories;
using Demelain.Server.Services;
using Demelain.Server.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Demelain.Server.Extensions
{
    /// <summary>
    /// Extension methods for configuring and adding services, utilities, etc. to the application's
    /// service container.
    /// </summary>
    public static class ServiceConfigurationExtensions
    {
        /// <summary>
        /// Adds an <see cref="ApplicationAuthContext"/> to the application's
        /// <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="configuration">The configured <see cref="IServiceCollection"/>.</param>
        public static void ConfigureDbContexts(this IServiceCollection services, 
            IConfiguration configuration)
        {
            // Configures the auth context
            services.AddDbContext<ApplicationAuthContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ApplicationAuthContext")));
            
            // Configures the app context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ApplicationDbContext")));
        }

        /// <summary>
        /// Adds an <see cref="IUnitOfWork"/> to the application's <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public static void ConfigureLocalServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPersonalDetailService, PersonalDetailsService>();
        }

        /// <summary>
        /// Adds an <see cref="ILocalFileUtility"/> to the application's <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public static void ConfigureUtilities(this IServiceCollection services)
        {
            services.AddTransient<ILocalFileUtility, LocalFileUtility>();
        }
    }
}