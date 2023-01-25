
using OptiBid.Microservices.Auction.Domain.Entities;

namespace OptiBid.Microservices.Auction.Data.Repositories
{
    public interface IAuctionAssetsRepository:IRepository<AuctionAsset>
    {
        Task<AuctionAsset> GetById(int id,CancellationToken cancellationToken=default);
        Task Delete(AuctionAsset auctionAsset, CancellationToken cancellationToken = default);
    }
}
