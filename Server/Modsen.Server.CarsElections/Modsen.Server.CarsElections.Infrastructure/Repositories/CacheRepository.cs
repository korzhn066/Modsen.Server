using Microsoft.Extensions.Caching.Distributed;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Modsen.Server.CarsElections.Infrastructure.Repositories
{
    public class CacheRepository(IDistributedCache distributedCache) : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache = distributedCache;
        private readonly JsonSerializerOptions jsonSerializerOptions = new() { ReferenceHandler = ReferenceHandler.IgnoreCycles };

        public async Task SetAsync<T>(string key, T value, int ttl)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(ttl),
            };

            var data = JsonSerializer.Serialize(value, jsonSerializerOptions);

            await _distributedCache.SetStringAsync(key, data, options);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var data = JsonSerializer.Serialize(value, jsonSerializerOptions);

            await _distributedCache.SetStringAsync(key, data);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var data = await _distributedCache.GetStringAsync(key);

            if (data is null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T?>(data, jsonSerializerOptions);
        }
    }
}