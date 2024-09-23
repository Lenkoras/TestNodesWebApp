using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace TestNodesWeb
{
    public static class SwaggerGenServiceCollectionExtensions
    {
        private static readonly string DocsVersion = "1.0.0";

        public static IServiceCollection AddSwaggerGenWithDocs(this IServiceCollection services)
        {
            string? xmlName = Assembly.GetCallingAssembly().GetName().Name;
            services.AddSwaggerGen(delegate (SwaggerGenOptions options)
            {
                options.SwaggerDoc("v1", new() { Version = DocsVersion, Title = "Swagger" });
                if (xmlName != null)
                {
                    string filePath = Path.Combine(AppContext.BaseDirectory, xmlName + ".xml");
                    options.IncludeXmlComments(filePath);
                }
            });
            return services;
        }
    }
}