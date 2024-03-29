using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using TechHoy.Core;

namespace TechHoy.MemoryCache
{
    public class RedisMemoryCache(IConfiguration configuration) : IMemoryCache
    {
        ConnectionMultiplexer? redis = null;
        IDatabase? db = null;
        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        public async Task<string> GetAsync(string address)
        {
            if (db == null)
                if (!Connect())
                    throw new Exception("Redis not connect");
            return (await db.StringGetAsync(address)).ToString();
        }

        public Task PutAsync(string address, string value)
        {
            if (db == null)
                if (!Connect())
                    throw new Exception("Redis not connect");
            return db.StringSetAsync(address, value);
        }

        private bool Connect()
        {
            try
            {
                redis = ConnectionMultiplexer.Connect(_configuration["MemoryCacheConnectionString"]);
                db = redis.GetDatabase();
                return db.Ping() < TimeSpan.FromSeconds(2);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
