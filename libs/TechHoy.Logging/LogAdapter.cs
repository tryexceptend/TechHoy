using Microsoft.Extensions.Logging;
using RestSharp;

namespace TechHoy.Logging;

public class LogAdapter : ILogger, IDisposable
{
    private readonly RestClient _client;
    private readonly string _categoryName;
    private readonly string _applicationName;
    public LogAdapter(string applicationName, string categoryName)
    {
        var options = new RestClientOptions("http://127.0.0.1:10900")
        {
        };
        _client = new RestClient(options);
        _categoryName = categoryName;
        _applicationName = applicationName;
    }
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }

    public void Dispose() => _client.Dispose();

    public bool IsEnabled(LogLevel logLevel)
    {
        try
        {
            var request = new RestRequest("/health");
            var response = _client.GetAsync(request, default).GetAwaiter().GetResult();
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {

        }
        return false;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        try
        {
            LogMessage message = new()
            {
                Message = (string.IsNullOrEmpty(eventId.Name) ? "" : eventId.Name + " - ") + formatter(state, exception),
                Level = logLevel,
                Source = _categoryName,
                Application = _applicationName
            };
            var request = new RestRequest("/log/add").AddJsonBody(message);
            _client.PutAsync(request).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
