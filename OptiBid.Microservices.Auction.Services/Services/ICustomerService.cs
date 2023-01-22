
using OptiBid.Microservices.Auction.Services.Models;

namespace OptiBid.Microservices.Auction.Services.Services
{
    public interface ICustomerService
    {
        Task<CustomerResponse> Get(int userId,CancellationToken cancellationToken=default);
        Task<CustomerResponse> Get(CancellationToken cancellationToken = default);
        Task<CustomerResponse> Add(Domain.Input.Customer customer,CancellationToken cancellationToken=default);
    }
}
