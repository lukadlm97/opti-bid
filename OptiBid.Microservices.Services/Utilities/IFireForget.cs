using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Shared.Caching.Hybrid;

namespace OptiBid.Microservices.Services.Utilities
{
    public interface IFireForget<T> where T : class
    {
        void Execute(Func<IHybridCache<T>, Task> asyncWork);
    }
}
