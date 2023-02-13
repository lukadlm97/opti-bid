

namespace OptiBid.Microservices.Shared.Caching.Distributed
{
    public interface IDistributedCache<T>
    {
        Task Set(string key, T value, CancellationToken cancellationToken = default);
        Task<T> Get(string key, CancellationToken cancellationToken = default);
        Task Invalidate(string key, CancellationToken cancellationToken = default);
    }
}
