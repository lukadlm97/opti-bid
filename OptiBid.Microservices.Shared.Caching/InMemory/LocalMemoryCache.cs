using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Shared.Caching.Configuration;
using OptiBid.Microservices.Shared.Caching.Utilities;
using Microsoft.Extensions.Logging;

namespace OptiBid.Microservices.Shared.Caching.InMemory
{
    public class LocalMemoryCache<T>:ILocalMemoryCache<T> where T : class
    {
        private readonly HybridCacheSettings _hybridCacheSettings;
        private readonly IMemoryCache _memoryCache;
        private readonly CacheSignal<T> _cacheSignal;
        private readonly ILogger<LocalMemoryCache<T>> _logger;

        public LocalMemoryCache(IOptions<HybridCacheSettings> options,IMemoryCache memoryCache, CacheSignal<T> cacheSignal,ILogger<LocalMemoryCache<T>> logger)
        {
            this._hybridCacheSettings = options.Value;
            this._memoryCache = memoryCache;
            this._cacheSignal = cacheSignal;
            this._logger = logger;
        }
        public async Task Set(string key, T value, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cacheSignal.WaitAsync();

                var completeKey = _hybridCacheSettings.ApplicationName + key;
                var memoryCacheEntryOptions = DeterminateCacheEntryOptions();
                _memoryCache.Set(completeKey, value, memoryCacheEntryOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                _cacheSignal.Release();
            }
        }

        public async Task<T> Get(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cacheSignal.WaitAsync();

                var completeKey = _hybridCacheSettings.ApplicationName + key;
                return _memoryCache.Get<T>(completeKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                _cacheSignal.Release();
            }

            return null;
        }

        public async Task Invalid(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cacheSignal.WaitAsync();

                var completeKey = _hybridCacheSettings.ApplicationName + key;
                _memoryCache.Remove(completeKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                _cacheSignal.Release();
            }
            
        }

        MemoryCacheEntryOptions DeterminateCacheEntryOptions()
        {
            if (_hybridCacheSettings.IsSlidingExpiration)
            {
                return new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = _hybridCacheSettings.LocalCacheTime
                };
            }
            return new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = _hybridCacheSettings.LocalCacheTime
            };
        }
    }
}
