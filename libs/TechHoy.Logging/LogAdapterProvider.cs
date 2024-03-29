using Microsoft.Extensions.Logging;

namespace TechHoy.Logging
{
    public class LogAdapterProvider : ILoggerProvider
    {

        public ILogger CreateLogger(string categoryName)
        {
            string fName = Path.GetFileName(path: Environment.ProcessPath);
            return new LogAdapter(fName, categoryName);
        }

        public void Dispose() { }
    }
}
