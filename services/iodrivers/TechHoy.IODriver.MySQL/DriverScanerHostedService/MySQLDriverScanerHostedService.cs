using TechHoy.Core;

namespace TechHoy.IODriver.MySQL.DriverScanerHostedService;

public class MySQLDriverScanerHostedService(
    IConfiguration configuration,
    MySQLContext context,
    IMemoryCache memoryCache,
    ILogger<MySQLDriverScanerHostedService> logger) : BackgroundService
{
    private readonly MySQLContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    private readonly ILogger<MySQLDriverScanerHostedService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMemoryCache _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            int timeout = int.Parse(_configuration["TimeOutSec"] ?? "10");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var addresses = _context.Addresses.Where(x => x.Enabled == true).ToList();
                    foreach (var address in addresses)
                    {
                        await _memoryCache.PutAsync(address.AddressId, address.Value);
                    }
                }
                catch { }
                await Task.Delay(TimeSpan.FromSeconds(timeout));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, null);
        }
    }
}
