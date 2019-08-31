using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Meyer.Common.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcCoreBuilder AddMvcMin(this IServiceCollection services)
        {
            return services
                .AddMvcCore()
                .AddJsonFormatters(x =>
                {
                    x.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    x.NullValueHandling = NullValueHandling.Ignore;
                    x.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
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