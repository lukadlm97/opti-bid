using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Shared.Caching.Configuration
{
    public class LocalCacheSettings
    {
        public TimeSpan LocalCacheTime { get; set; }
        public bool IsSlidingExpiration { get; set; }
    }
}
