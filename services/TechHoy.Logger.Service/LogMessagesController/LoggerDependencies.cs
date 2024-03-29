using Microsoft.EntityFrameworkCore;

namespace TechHoy.Logger.LogMessagesController;

public static class LoggerDependencies
{
    public static IServiceCollection AddLoggerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // получаем строку подключения
        var connectionString = configuration.GetConnectionString("LogsConnectionString");
        services.AddScoped(provider =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<LogMessagesContext>();
            return optionsBuilder.UseSqlite(connectionString).Options;
        });
        services.AddScoped<LogMessagesContext>();
        return services;
    }
}
