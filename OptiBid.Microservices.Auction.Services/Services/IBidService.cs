using OptiBid.Microservices.Auction.Services.Models;

namespace OptiBid.Microservices.Auction.Services.Services
{
    public interface IBidService
    {
        Task<BidResponse> Add(Domain.Input.Bid bid,CancellationToken cancellationToken=default);
        Task<BidResponse> GetByAssetsById(int assetId, CancellationToken cancellationToken = default);

    }
}
