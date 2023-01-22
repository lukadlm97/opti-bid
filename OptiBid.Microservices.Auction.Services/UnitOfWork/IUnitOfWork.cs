using OptiBid.Microservices.Auction.Data.Repositories;

namespace OptiBid.Microservices.Auction.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Microservices.Auction.Domain.Entities.ProductCategory> _productCategoryRepository { get; }

        IRepository<Microservices.Auction.Domain.Entities.ServiceCategory> _serviceCategoryRepository { get; }
        ICustomerRepository _customerRepository { get; }

        Task Commit(CancellationToken cancellationToken = default);
    }
}
