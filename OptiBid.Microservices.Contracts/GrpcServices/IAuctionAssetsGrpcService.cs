
namespace OptiBid.Microservices.Contracts.GrpcServices
{
    public interface IAuctionAssetsGrpcService
    {
        Task<IEnumerable<Domain.Output.Auction.Asset>> Get(Domain.Input.PagingRequest pagingRequest,CancellationToken cancellationToken = default);
        Task<Domain.Output.Auction.Asset> Get(int id,CancellationToken cancellationToken = default);
        Task<Domain.Output.Auction.UpsertResult> Insert(Domain.Input.UpsertAssetRequest assetRequest,CancellationToken cancellationToken = default);
        Task<Domain.Output.Auction.UpsertResult> Update(int id,Domain.Input.UpsertAssetRequest assetRequest,CancellationToken cancellationToken=default);
        Task<Domain.Output.Auction.UpsertResult> Delete(int id,CancellationToken cancellationToken=default);

        Task<Domain.Output.Auction.BidReply> AddBid(int assetId,string username,Domain.Input.BidRequest bidRequest,CancellationToken cancellationToken = default);
        Task<IEnumerable<Domain.Output.Auction.BidDetailsReply>> GetBids(int assetId,CancellationToken cancellationToken = default);
    }
}
