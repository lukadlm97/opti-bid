using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace OptiBid.Microservices.Shared.Caching.Factory
{
    public interface IDistributedCacheConnectionFactory
    {
        IDatabase GetConnection();
    }
}
