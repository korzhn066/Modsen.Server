using Microsoft.Extensions.Caching.Distributed;
using Modsen.Server.CarsElections.Domain.Interfaces.Repositories;
using System.Text.Json;

namespace Modsen.Server.CarsElections.Infrastructure.Repositories
{
    public class CacheRepository(IDistributedCache distributedCache) : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache = distributedCache;

        public async Task SetAsync<T>(string key, T value, int ttl)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(ttl),
            };

            var data = JsonSerializer.Serialize(value);

            await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(data), options);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var data = JsonSerializer.Serialize(value);

            await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(data));
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var data = await _distributedCache.GetStringAsync(key);

            return JsonSerializer.Deserialize<T>(data!);
        }
    }
}