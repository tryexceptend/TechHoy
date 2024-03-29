using Microsoft.EntityFrameworkCore;

namespace TechHoy.IODriver.MySQL.DriverScanerHostedService;

public class MySQLContext : DbContext
{
    public DbSet<MySQLDriverValue> Addresses => Set<MySQLDriverValue>();
    public MySQLContext(DbContextOptions<MySQLContext> options)
            : base(options)
    {
        Database.EnsureCreated();
    }
}
