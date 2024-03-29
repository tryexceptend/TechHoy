using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using TechHoy.Logger.LogMessagesController;

namespace TechHoy.Logger.Service.HealthController;

public static class HealthCheckableDependencies
{
    public static IHealthChecksBuilder AddDataBaseHealthCheck(this IHealthChecksBuilder builder, HealthStatus? failureStatus = null, IEnumerable<string> tags = null)
    {
        return builder.Add(new HealthCheckRegistration("sqlite_health_check", (IServiceProvider sp) => sp.GetService<LogMessagesContext>(), failureStatus, tags));
    }

    public static IApplicationBuilder UseCryptoHealthChecks(this IApplicationBuilder app, PathString path)
    {
        app.UseHealthChecks(path, new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(report));
            }
        });
        return app;
    }
}
