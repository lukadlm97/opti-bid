using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using OptiBid.Microservices.Auction.Grpc.Profiles.ValueResolver;

namespace OptiBid.Microservices.Auction.Grpc.Profiles
{
    public class AuctionAssetsProfile:Profile
    {
        public AuctionAssetsProfile()
        {
            CreateMap<AuctionAssetsServiceDefinition.AddAssetRequest, Domain.Input.AuctionAsset>()
                .ForMember(dest=>dest.ProductTypeId,opt=>opt.MapFrom<ValueResolver.ProductTypeResolver>())
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom<ValueResolver.ServiceTypeResolver>())
                .ForMember(dest=>dest.MediaUrl,opt=>opt.MapFrom<MediaUrlResolver>())
                .ForMember(dest=>dest.StartDate,
                    opt=>opt.MapFrom(src=>src.StartDate.ToDateTime()))
                .ForMember(dest => dest.EndDate,
                    opt => opt.MapFrom(src => src.EndDate.ToDateTime()))
                .ForMember(dest=>dest.CustomerId,opt=>opt.MapFrom(src=>src.CustomerId))
                ;
            CreateMap<Domain.DTOs.AuctionAsset, AuctionAssetsServiceDefinition.SingleAssetReply>()
                .ForMember(src=>src.StartDate,
                    opt=>opt.MapFrom(src=>src.StartDate.Value.ToTimestamp()))
                .ForMember(src => src.EndDate,
                    opt => opt.MapFrom(src => src.EndDate.ToTimestamp()))
                ;
        }

       
    }
}
