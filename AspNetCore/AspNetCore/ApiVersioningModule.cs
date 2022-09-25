using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Meyer.Common.AspNetCore;

public static class ApiVersioningModule
{
    public static IServiceCollection AddApiVersioning(this IServiceCollection services, int defaultMajor = 1, int defaultMinor = 0)
    {
        services.AddApiVersioning(o =>
        {
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.ApiVersionReader = new QueryStringApiVersionReader("v");
            o.DefaultApiVersion = new ApiVersion(defaultMajor, defaultMinor);
            o.ReportApiVersions = true;
        });

        return services;
    }
}