using Microsoft.Extensions.Configuration;

namespace TechHoy.CustomApplication;

public static class ConfigurationCreater
{
    public static IConfiguration BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json");

        if (File.Exists("appsettings.MemoryCache.json"))
            builder.AddJsonFile($"appsettings.MemoryCache.json", optional: true);

        return builder.Build();
    }

}
