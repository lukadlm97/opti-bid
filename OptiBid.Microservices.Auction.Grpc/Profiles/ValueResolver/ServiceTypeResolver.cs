using AutoMapper;
using OptiBid.Microservices.Auction.Domain.Input;
using OptiBid.Microservices.Auction.Grpc.AuctionAssetsServiceDefinition;

namespace OptiBid.Microservices.Auction.Grpc.Profiles.ValueResolver
{
    public class ServiceTypeResolver : IValueResolver<AuctionAssetsServiceDefinition.AddAssetRequest, Domain.Input.AuctionAsset, int?>
    {
        public int? Resolve(AddAssetRequest source, AuctionAsset destination, int? destMember, ResolutionContext context)
        {
            return source.CategoryCase switch
            {
                AddAssetRequest.CategoryOneofCase.ServiceTypeId => source.ServiceTypeId,
                _ => null
            };
        }
    }
}
