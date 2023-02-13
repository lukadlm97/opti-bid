namespace OptiBid.Microservices.Shared.Caching.Configuration
{
    public class DistributedCacheSettings
    {
        public string ServerName { get; set; }
        public int Port { get; set; }
        public int DbNumber { get; set; }
        public TimeSpan TTL { get; set; }
    }
}
