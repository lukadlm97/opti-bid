

namespace OptiBid.Microservices.Shared.Caching.Configuration
{
    public class HybridCacheSettings
    {
        public string ApplicationName { get; set; }
        public TimeSpan LocalCacheTime { get; set; }
        public TimeSpan DistributedCacheTime { get; set; }
        public bool IsSlidingExpiration { get; set; }
    }
}
