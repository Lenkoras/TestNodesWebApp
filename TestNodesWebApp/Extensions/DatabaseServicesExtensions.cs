using Database;
using Microsoft.EntityFrameworkCore;

namespace TestNodesWeb
{
    public static class DatabaseServicesExtensions
    {
        /// <summary>
        /// Adds the database context from the specified <paramref name="configuration"/>.
        /// </summary>
        /// <param name="services">The collection to which all database services have to be added.</param>
        /// <param name="configuration">Configuration with the database connection string.</param>
        /// <returns>The same service collection that was specified.</returns>
        /// <exception cref="ArgumentException">When <i>DefaultConnection</i> was not specified in the <paramref name="configuration"/>.</exception>
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentException(nameof(connectionString));

            services.AddDbContext<ApplicationDbContext>(config => config.UseNpgsql(connectionString));

            return services;
        }
    }
}
