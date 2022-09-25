using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Meyer.Common.AspNetCore;

public static class HealthCheckModule
{
    public static IServiceCollection AddHealthCheckModule(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("health", () => HealthCheckResult.Healthy(), new[] { "up" });

        return services;
    }

    public static IEndpointRouteBuilder MapHealthCheckEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("up")
        })
        .WithMetadata(new AllowAnonymousAttribute());

        return app;
    }
}