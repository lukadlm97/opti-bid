using AutoMapper;
using OptiBid.Microservices.Auction.Domain.Input;
using OptiBid.Microservices.Auction.Grpc.AuctionAssetsServiceDefinition;

namespace OptiBid.Microservices.Auction.Grpc.Profiles.ValueResolver
{
    public class MediaUrlResolver : IValueResolver<AuctionAssetsServiceDefinition.AddAssetRequest, Domain.Input.AuctionAsset, IEnumerable<string>>
    {
        public IEnumerable<string> Resolve(AddAssetRequest source, AuctionAsset destination, IEnumerable<string> destMember, ResolutionContext context)
        {
            return source.MediaUrl.Select(x => x);
        }
    }
}
