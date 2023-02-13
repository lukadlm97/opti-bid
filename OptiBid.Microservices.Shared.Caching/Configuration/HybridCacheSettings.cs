

namespace OptiBid.Microservices.Shared.Caching.Configuration
{
    public class HybridCacheSettings
    {
        public string ApplicationName { get; set; }
        public DistributedCacheSettings DistributedCacheSettings { get; set; }
        public LocalCacheSettings LocalCacheSettings { get; set; }
    }
}
