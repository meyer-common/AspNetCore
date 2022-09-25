using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Meyer.Common.AspNetCore;

public static class JsonConfigurationModule
{
    public static IServiceCollection AddJsonConfiguration(this IServiceCollection services)
    {
        return services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }
}