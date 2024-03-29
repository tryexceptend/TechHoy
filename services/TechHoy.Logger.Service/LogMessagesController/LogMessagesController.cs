using Microsoft.AspNetCore.Mvc;
using TechHoy.Logging;

namespace TechHoy.Logger.LogMessagesController;

/// <summary>
/// Контроллер доступа к логам
/// </summary>
/// <param name="context"></param>
[Route("log")]
[ApiController]
public class LogMessagesController(LogMessagesContext context) : Controller
{
    private readonly LogMessagesContext _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>
    /// Добавить запись в лог
    /// </summary>
    /// <param name="logMessage">Сообщение</param>
    /// <returns></returns>
    [HttpPut("add")]
    public Task AddMessageAsync([FromBody] LogMessage logMessage)
    {
        try
        {
            if (logMessage == null) return Task.CompletedTask;
            _context.Messages.Add(logMessage);
            Console.WriteLine(logMessage.ToString());
            return _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return Task.CompletedTask;
    }
}
