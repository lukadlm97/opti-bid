using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Shared.Caching.InMemory
{
    public interface ILocalMemoryCache<T>
    where T : class
    {
        Task Set(string key, T value,CancellationToken cancellationToken=default);
        Task<T> Get(string key, CancellationToken cancellationToken = default);
        Task Invalidate(string key, CancellationToken cancellationToken = default); 
        Task Set(string key, List<T> values, CancellationToken cancellationToken = default);
        Task<List<T>> GetCollection(string key, CancellationToken cancellationToken = default);

    }
}
