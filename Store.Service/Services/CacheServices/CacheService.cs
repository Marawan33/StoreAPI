using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Service.Services.CacheServices
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<string> GetCacheResponseAsync(string key)
        {
            var CachedResponse = await _database.StringGetAsync(key);
            if (CachedResponse.IsNullOrEmpty)
                return null;
            return CachedResponse.ToString();
        }

        public async Task SetCacheResponseAsync(string key, object responseTime, TimeSpan timeToLive)
        {
            if (responseTime is null)
                return;
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var serializedResponse = JsonSerializer.Serialize(responseTime, options);
            await _database.StringSetAsync(key, serializedResponse, timeToLive);
        }
    }
}
