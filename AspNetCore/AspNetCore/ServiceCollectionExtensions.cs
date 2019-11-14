using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Meyer.Common.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddMvcMin(this IServiceCollection services)
        {
            services.AddControllers();

            return services
                .AddRazorPages()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.IgnoreNullValues = true;
                    x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
        }

        public static IServiceCollection AddApiVersioning(this IServiceCollection services, int defaultMajor = 1, int defaultMinor = 0)
        {
            return services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new QueryStringApiVersionReader(parameterNames: "v");
                options.DefaultApiVersion = new ApiVersion(defaultMajor, defaultMinor);
                options.ReportApiVersions = true;
            });
        }
    }
}