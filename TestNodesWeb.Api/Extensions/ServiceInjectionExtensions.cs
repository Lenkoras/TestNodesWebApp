using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Reflection;
using TestNodesWeb.Api.Data.Repositories;

namespace TestNodesWeb
{
    public static class ServiceInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            Assembly assemblyInstance = typeof(ServiceInjectionExtensions).Assembly;

            return services.AddMediatR(MediatRConfig)
                .AddHttpContextAccessor()
                .AddDatabaseContext(configuration)
                .AddAutoMapper(assemblyInstance)
                .AddScoped<EnableRequestBufferingMiddleware>()
                .AddScoped<IJournalRepository, JournalRepository>()
                .AddScoped<INodeRepository, NodeRepository>()
                .AddCors(SetupCors);
        }

        private static void SetupCors(CorsOptions opts) =>
            opts.AddPolicy(Environments.Development, policy =>
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    );

        private static void MediatRConfig(MediatRServiceConfiguration config)
        {
            config.RegisterServicesFromAssembly(typeof(ServiceInjectionExtensions).Assembly);
            config.Lifetime = ServiceLifetime.Transient;
        }
    }
}