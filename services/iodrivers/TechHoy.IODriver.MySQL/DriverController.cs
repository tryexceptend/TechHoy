using Microsoft.AspNetCore.Mvc;
using TechHoy.IODriver.MySQL.DriverScanerHostedService;

namespace TechHoy.IODriver.MySQL;

/// <summary>
/// Контроллер доступа к логам
/// </summary>
/// <param name="context"></param>
[Route("driver")]
[ApiController]
public class DriverController(MySQLContext context) : Controller
{
    private readonly MySQLContext _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// Добавить запись в лог
    /// </summary>
    /// <param name="logMessage">Сообщение</param>
    /// <returns></returns>
    [HttpPut("add")]
    public Task AddMessageAsync([FromBody] MySQLDriverValue value)
    {
        try
        {
            if (value == null) return Task.CompletedTask;
            value.Enabled = true;
            value.DtUpdate = DateTime.UtcNow;
            _context.Addresses.Add(value);
            Console.WriteLine(value);
            return _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return Task.CompletedTask;
    }
}
