
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Auction.Data.Repositories;

namespace OptiBid.Microservices.Auction.Services.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Microservices.Auction.Domain.Entities.ProductCategory> ProductCategoryRepository { get; }

        IRepository<Microservices.Auction.Domain.Entities.ServiceCategory> ServiceCategoryRepository { get; }

        Task Commit(CancellationToken cancellationToken = default);
    }
}
