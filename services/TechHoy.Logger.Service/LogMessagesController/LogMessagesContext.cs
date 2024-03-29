using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TechHoy.Logging;

namespace TechHoy.Logger.LogMessagesController;

public class LogMessagesContext : DbContext, IHealthCheck
{
    public DbSet<LogMessage> Messages => Set<LogMessage>();
    public LogMessagesContext(DbContextOptions<LogMessagesContext> options)
            : base(options)
    {
        Database.EnsureCreated();
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Database.CanConnect()
            ? HealthCheckResult.Healthy("OK")
            : HealthCheckResult.Unhealthy("Database ensure not created"));
    }
}
