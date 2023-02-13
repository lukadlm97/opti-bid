
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Shared.Caching.Configuration;
using OptiBid.Microservices.Shared.Caching.Distributed;
using OptiBid.Microservices.Shared.Caching.InMemory;

namespace OptiBid.Microservices.Shared.Caching.Hybrid
{
    internal class HybridCache<T>:IHybridCache<T> where T : class
    {
        private readonly HybridCacheSettings _hybridCacheSettings;
        private readonly ILocalMemoryCache<T> _localMemoryCache;
        private readonly IDistributedCache<T> _distributedCache;
        private readonly ILogger<HybridCache<T>> _logger;

        public HybridCache(IOptions<HybridCacheSettings> options,ILocalMemoryCache<T> localMemoryCache,IDistributedCache<T> distributedCache,ILogger<HybridCache<T>> logger)
        {
            this._hybridCacheSettings = options.Value;
            this._localMemoryCache = localMemoryCache;
            this._distributedCache = distributedCache;
            this._logger = logger;
        }
        public async Task Set(string key, T value, CancellationToken cancellationToken = default)
        {
            try
            {
                var completeKey = _hybridCacheSettings.ApplicationName + key;
                await _localMemoryCache.Set(completeKey, value, cancellationToken);
                await _distributedCache.Set(completeKey, value, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<T> Get(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var completeKey = _hybridCacheSettings.ApplicationName + key;
                var objFromCache = await _localMemoryCache.Get(completeKey, cancellationToken);
                if (objFromCache != null)
                    return objFromCache;
                
                objFromCache = await _distributedCache.Get(completeKey, cancellationToken);
                return objFromCache;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task Invalidate(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var completeKey = _hybridCacheSettings.ApplicationName + key;
                await _localMemoryCache.Invalidate(completeKey, cancellationToken);
                await _distributedCache.Invalidate(completeKey, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task Set(string key, List<T> values, CancellationToken cancellationToken = default)
        {
            try
            {
                await _localMemoryCache.Set(key, values, cancellationToken);
                await _distributedCache.Set(key, values, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<List<T>> GetCollection(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                var objFromCache = await _localMemoryCache.GetCollection(key, cancellationToken);
                if (objFromCache != null)
                    return objFromCache;

                objFromCache = await _distributedCache.GetCollection(key, cancellationToken);
                return objFromCache;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
        
    }
}
