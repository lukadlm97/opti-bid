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
        Task Invalid(string key, CancellationToken cancellationToken = default);

    }
}
