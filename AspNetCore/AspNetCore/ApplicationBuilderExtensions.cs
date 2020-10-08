using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Mime;
using System.Reflection;

namespace Meyer.Common.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomHealthCheck(this IApplicationBuilder builder, string apiName, string path)
        {
            var healthCheckOptions = new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var result = JsonConvert.SerializeObject(new
                    {
                        api = apiName,
                        version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                        health_checks = report.Entries.Select(e => new
                        {
                            description = e.Key,
                            state = (e.Value.Status == HealthStatus.Healthy).ToString(),
                        }),
                    });

                    await context.Response.WriteAsync(result);
                }
            };

            return builder.UseHealthChecks(new PathString(path), healthCheckOptions);
        }
    }
}
