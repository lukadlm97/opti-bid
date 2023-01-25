using OptiBid.Microservices.Auction.Domain.Entities;

namespace OptiBid.Microservices.Auction.Data.Repositories
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Task<Customer?> FindByIdAsync(int userId,CancellationToken cancellationToken=default);
        Task<Customer?> FindById(int id, CancellationToken cancellationToken = default);
    }
}
