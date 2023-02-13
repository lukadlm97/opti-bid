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
                
                var memoryCacheEntryOptions = DeterminateCacheEntryOptions();
                _memoryCache.Set(key, value, memoryCacheEntryOptions);
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
                
                return _memoryCache.Get<T>(key);
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

        public async Task Invalidate(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cacheSignal.WaitAsync();
                
                _memoryCache.Remove(key);
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

        public async Task Set(string key, List<T> values, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cacheSignal.WaitAsync();

                var memoryCacheEntryOptions = DeterminateCacheEntryOptions();
                _memoryCache.Set(key, values, memoryCacheEntryOptions);
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

        public async Task<List<T>> GetCollection(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cacheSignal.WaitAsync();

                return _memoryCache.Get<List<T>>(key);
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

        MemoryCacheEntryOptions DeterminateCacheEntryOptions()
        {
            if (_hybridCacheSettings.LocalCacheSettings.IsSlidingExpiration)
            {
                return new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = _hybridCacheSettings.LocalCacheSettings.LocalCacheTime
                };
            }
            return new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = _hybridCacheSettings.LocalCacheSettings.LocalCacheTime
            };
        }
    }
}
