using OptiBid.Microservices.Shared.Caching.Configuration;
using OptiBid.Microservices.Shared.Caching.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OptiBid.Microservices.Shared.Caching.Utilities
{
    public static class CacheInitializationExtensions
    {
        public static void AddHybridCaching(this IServiceCollection serviceProvider, HostBuilderContext hostingContext)
        {
            serviceProvider.Configure<HybridCacheSettings>(hostingContext.Configuration.GetSection(nameof(HybridCacheSettings)));
            serviceProvider.AddMemoryCache();
            serviceProvider.AddLogging();
            serviceProvider.AddSingleton(typeof(ILocalMemoryCache<>), typeof(LocalMemoryCache<>));
            serviceProvider.AddSingleton(typeof(CacheSignal<>));
        }
    }
}
