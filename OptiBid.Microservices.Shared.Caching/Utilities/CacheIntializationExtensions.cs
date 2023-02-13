using OptiBid.Microservices.Shared.Caching.Configuration;
using OptiBid.Microservices.Shared.Caching.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OptiBid.Microservices.Shared.Caching.Distributed;
using OptiBid.Microservices.Shared.Caching.Factory;
using OptiBid.Microservices.Shared.Caching.Hybrid;

namespace OptiBid.Microservices.Shared.Caching.Utilities
{
    public static class CacheInitializationExtensions
    {
        public static void AddHybridCaching(this IServiceCollection serviceProvider, HostBuilderContext hostingContext)
        {
            serviceProvider.Configure<HybridCacheSettings>(hostingContext.Configuration.GetSection(nameof(HybridCacheSettings)));
            serviceProvider.AddMemoryCache();
            serviceProvider.AddLogging();
            serviceProvider.AddSingleton(typeof(IDistributedCacheConnectionFactory), typeof(RedisConnectionFactory));
            serviceProvider.AddSingleton(typeof(ILocalMemoryCache<>), typeof(LocalMemoryCache<>));
            serviceProvider.AddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
            serviceProvider.AddSingleton(typeof(IHybridCache<>), typeof(HybridCache<>));
            serviceProvider.AddSingleton(typeof(CacheSignal<>));
        }
    }
}
