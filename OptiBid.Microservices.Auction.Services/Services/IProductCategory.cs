using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Auction.Services.Services
{
    public interface IProductCategory
    {
        Task<IEnumerable<Domain.DTOs.Category>> Get(CancellationToken cancellationToken = default);
    }
}
