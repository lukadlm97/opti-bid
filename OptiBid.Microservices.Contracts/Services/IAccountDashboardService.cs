using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Contracts.Domain.Output;

namespace OptiBid.Microservices.Contracts.Services
{
    public interface IAccountDashboardService
    {
        Task<IEnumerable<EnumItem>> GetCountries(CancellationToken cancellationToken);
    }
}
