using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Meyer.Common.AspNetCore;

public static class LoggingModule
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((hostBuilderContext, loggerContext) => loggerContext
            .ReadFrom.Configuration(hostBuilderContext.Configuration, "Serilog:Console")
        );

        return builder;
    }

    public static WebApplication UseSerilogRequestLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging(o => { o.MessageTemplate = "Handled {RequestPath} {StatusCode}"; });

        return app;
    }
}