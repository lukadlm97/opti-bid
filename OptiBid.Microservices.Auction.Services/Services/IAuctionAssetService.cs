
namespace OptiBid.Microservices.Auction.Services.Services
{
    public interface IAuctionAssetService
    {
        Task<Models.AuctionAssetsResponse> GetAll(CancellationToken cancellationToken=default);
        Task<Models.AuctionAssetsResponse> GetById(int id,CancellationToken cancellationToken = default);
        Task<Models.AuctionAssetsResponse> Create(Domain.Input.AuctionAsset auctionAsset,CancellationToken cancellationToken=default);
        Task<Models.AuctionAssetsResponse> Update(Domain.Input.AuctionAsset auctionAsset,
            CancellationToken cancellationToken = default);
        Task<Models.AuctionAssetsResponse> Delete(int id,CancellationToken cancellationToken=default);
    }
}
