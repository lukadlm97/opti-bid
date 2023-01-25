using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Auction.Domain.Entities;
using OptiBid.Microservices.Auction.Persistence;

namespace OptiBid.Microservices.Auction.Data.Repositories
{
    public class CustomerRepository:Repository<Customer>,ICustomerRepository
    {
        private readonly AuctionContext _auctionContext;

        public CustomerRepository(AuctionContext auctionContext) : base(auctionContext)
        {
            _auctionContext = auctionContext;
        }

        public async Task<Customer?> FindByIdAsync(int userId,CancellationToken cancellationToken=default)
        {
            return await _auctionContext.Customers.FirstOrDefaultAsync(x => x.UserID == userId,cancellationToken);
        }

        public async Task<Customer?> FindById(int id, CancellationToken cancellationToken = default)
        {
            return await _auctionContext.Customers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
