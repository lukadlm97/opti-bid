using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Auction.Data.Repositories;
using OptiBid.Microservices.Auction.Domain.Entities;
using OptiBid.Microservices.Auction.Persistence;

namespace OptiBid.Microservices.Auction.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly AuctionContext _auctionContext;

        public UnitOfWork(AuctionContext auctionContext)
        {
            _auctionContext = auctionContext;
            _productCategoryRepository = new Repository<ProductCategory>(_auctionContext);
            _serviceCategoryRepository = new Repository<ServiceCategory>(_auctionContext);
            _customerRepository=new CustomerRepository(_auctionContext);
        }
        public IRepository<ProductCategory> _productCategoryRepository { get; }
        public IRepository<ServiceCategory> _serviceCategoryRepository { get; }
        public ICustomerRepository _customerRepository { get; }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            await _auctionContext.SaveChangesAsync(cancellationToken);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _auctionContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
