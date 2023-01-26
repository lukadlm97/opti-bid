using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace OptiBid.Microservices.Auction.Grpc.Profiles
{
    public class BidProfile:Profile
    {
        public BidProfile()
        {
            CreateMap<AuctionAssetsServiceDefinition.BidRequest, Domain.Input.Bid>()
                .ForMember(dest=>dest.BidDate,opt=>opt.MapFrom(src=>src.BidDate.ToDateTime()));

            CreateMap<Domain.DTOs.Bid, AuctionAssetsServiceDefinition.SingleBidDetails>()
                .ForMember(dest=>dest.BidId,opt=>opt.MapFrom(src=>src.ID))
                .ForMember(dest=>dest.BidDate,opt=>opt.MapFrom(src=>src.BidDate.ToTimestamp()))
                
                ;

            CreateMap<Domain.DTOs.Bid, AuctionAssetsServiceDefinition.SingleBidReplay>()
                .ForMember(dest => dest.BidId, opt => opt.MapFrom(src => src.ID));
        }

    }
}
