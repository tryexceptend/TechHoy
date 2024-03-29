using Microsoft.AspNetCore.Mvc;

namespace TechHoy.MemoryCache.Service.Test;

/// <summary>
/// Контроллер доступа к логам
/// </summary>
/// <param name="context"></param>
[Route("api/test")]
[ApiController]
public class TestController(ILogger<TestController> logger) : Controller
{
    private readonly ILogger<TestController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    /// <summary>
    /// Добавить запись в лог
    /// </summary>
    /// <param name="logMessage">Сообщение</param>
    /// <returns></returns>
    [HttpGet("log")]
    public Task LogAsync()
    {
        try
        {
            _logger.LogDebug("Test debug");
            _logger.LogInformation("Test info");
            _logger.LogWarning("Test warn");
            _logger.LogError("Test error");
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return Task.CompletedTask;
    }
}

