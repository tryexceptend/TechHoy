using Microsoft.Extensions.Logging;

namespace TechHoy.Logging;

public static class LogAdapterExtensions
{
    public static ILoggingBuilder AddLogAdapter(this ILoggingBuilder builder)
    {
        builder.AddProvider(new LogAdapterProvider());
        return builder;
    }
}
