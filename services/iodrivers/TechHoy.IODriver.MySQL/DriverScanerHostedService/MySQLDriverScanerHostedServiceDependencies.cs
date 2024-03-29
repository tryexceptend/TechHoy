using Microsoft.EntityFrameworkCore;

namespace TechHoy.IODriver.MySQL.DriverScanerHostedService
{
    public static class MySQLDriverScanerHostedServiceDependencies
    {
        public static IServiceCollection AddMySQLDriverScanerDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<MySQLContext>();
                // получаем строку подключения
                string connectionString = configuration.GetConnectionString("DriverConnectionString")
                    ?? throw new Exception("DriverConnectionString is null");

                return optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;
            });
            services.AddSingleton<MySQLContext>();
            services.AddHostedService<MySQLDriverScanerHostedService>();
            return services;
        }
    }
}
