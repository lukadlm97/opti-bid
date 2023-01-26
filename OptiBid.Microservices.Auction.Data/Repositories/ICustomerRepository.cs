using OptiBid.Microservices.Auction.Domain.Entities;

namespace OptiBid.Microservices.Auction.Data.Repositories
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Task<Customer?> FindByUserIdAsync(int userId,CancellationToken cancellationToken=default);
        Task<Customer?> FindByUsername(string username, CancellationToken cancellationToken = default);
        Task<Customer?> FindById(int id, CancellationToken cancellationToken = default);
    }
}
