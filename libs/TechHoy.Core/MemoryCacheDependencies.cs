using Microsoft.Extensions.DependencyInjection;
using TechHoy.MemoryCache;

namespace TechHoy.Core
{
    public static class MemoryCacheDependencies
    {
        public static IServiceCollection AddMemoryCacheDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IMemoryCache, RedisMemoryCache>();
            return services;
        }
    }
}
