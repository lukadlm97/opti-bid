
namespace OptiBid.Microservices.Contracts.GrpcServices
{
    public interface IAuctionAssetsGrpcService
    {
        Task<IEnumerable<Domain.Output.Auction.Asset>> Get(Domain.Input.PagingRequest pagingRequest,CancellationToken cancellationToken = default);
        Task<Domain.Output.Auction.Asset> Get(int id,CancellationToken cancellationToken = default);
    }
}
