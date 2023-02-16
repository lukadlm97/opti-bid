using OptiBid.Microservices.Contracts.Domain.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.GrpcServices
{
    public interface ICategoryEnumerationGrpcService
    {
        Task<IEnumerable<EnumItem>> GetProducts(CancellationToken cancellationToken = default);
        Task<IEnumerable<EnumItem>> GetCategories(CancellationToken cancellationToken = default);

    }
}
