using System.Text.Json;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Shared.Caching.Configuration;
using OptiBid.Microservices.Shared.Caching.Factory;

namespace OptiBid.Microservices.Shared.Caching.Distributed
{
    public class DistributedCache<T>:IDistributedCache<T> where T : class
    {
        private readonly HybridCacheSettings _hybridCacheSettings;
        private readonly IDistributedCacheConnectionFactory _distributedCacheConnectionFactory;

        public DistributedCache(IOptions<HybridCacheSettings> options,IDistributedCacheConnectionFactory distributedCacheConnectionFactory)
        {
            this._hybridCacheSettings = options.Value;
            this._distributedCacheConnectionFactory = distributedCacheConnectionFactory;
            
        }
        public async Task Set(string key, T value, CancellationToken cancellationToken = default)
        {
            try
            {
                var db =  _distributedCacheConnectionFactory.GetConnection();
                var json = JsonSerializer.Serialize(value);
                if (await db.StringSetAsync(key, json, _hybridCacheSettings.DistributedCacheSettings.TTL))
                {
                    return;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<T> Get(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var db = _distributedCacheConnectionFactory.GetConnection();
                var item = await db.StringGetAsync(key);
                if (!string.IsNullOrWhiteSpace(item))
                {
                    return JsonSerializer.Deserialize<T>(item);
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Invalidate(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var db = _distributedCacheConnectionFactory.GetConnection();
                await db.KeyDeleteAsync(key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Set(string key, List<T> values, CancellationToken cancellationToken = default)
        {
            try
            {
                var db = _distributedCacheConnectionFactory.GetConnection();
                var json = JsonSerializer.Serialize(values);
                if (await db.StringSetAsync(key, json, _hybridCacheSettings.DistributedCacheSettings.TTL))
                {
                    return;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<T>> GetCollection(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var db = _distributedCacheConnectionFactory.GetConnection();
                var item = await db.StringGetAsync(key);
                if (!string.IsNullOrWhiteSpace(item))
                {
                    return JsonSerializer.Deserialize<List<T>>(item);
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
