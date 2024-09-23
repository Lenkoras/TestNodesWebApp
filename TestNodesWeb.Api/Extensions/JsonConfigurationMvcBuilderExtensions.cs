using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace TestNodesWeb
{
    public static class JsonConfigurationMvcBuilderExtensions
    {
        public static IMvcBuilder ConfigureJsonSerializer(this IMvcBuilder builder) =>
            builder.AddJsonOptions(ConfigureJson);

        private static void ConfigureJson(JsonOptions options) =>
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    }
}