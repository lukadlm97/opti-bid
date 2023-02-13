namespace OptiBid.Microservices.Shared.Caching.Hybrid
{
    public interface IHybridCache<T>
    where T : class
    {
        Task Set(string key, T value, CancellationToken cancellationToken = default);
        Task<T> Get(string key, CancellationToken cancellationToken = default);
        Task Invalidate(string key, CancellationToken cancellationToken = default);
        Task Set(string key, List<T> values, CancellationToken cancellationToken = default);
        Task<T> GetCollection(string key, CancellationToken cancellationToken = default);
        Task InvalidateCollection(string key, CancellationToken cancellationToken = default);
    }
}
