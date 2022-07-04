using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace poc_cache.Repositorios
{
    public class CacheRepositorio : ICacheRepositorio
    {
        private readonly IDistributedCache cache;
        private readonly DistributedCacheEntryOptions options;

        public CacheRepositorio(IDistributedCache cache)
        {
            this.cache = cache;
            this.options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(10),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };
        }

        public async Task Save<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize<T>(value);
            await cache.SetStringAsync(key, json, options);
        }

        public async Task Save(string key, string value)
        {
            await cache.SetStringAsync(key, value, options);
        }

        public async Task<IList<T>?> ObterTodos<T>(string collection)
        {
            var json = await cache.GetStringAsync(collection);
            if (json == null)
                return null;

            var todos = JsonSerializer.Deserialize<IList<T>>(json);
            return todos;
        }
    }
}
