
using OptiBid.Microservices.Auction.Domain.Entities;

namespace OptiBid.Microservices.Auction.Services.Services
{
    public interface IServiceCategory
    {
        Task<IEnumerable<Domain.DTOs.Category>> Get(CancellationToken cancellationToken = default);
    }
}
