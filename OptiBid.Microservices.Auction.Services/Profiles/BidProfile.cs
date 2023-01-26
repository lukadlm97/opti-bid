using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace OptiBid.Microservices.Auction.Services.Profiles
{
    public class BidProfile:Profile
    {
        public BidProfile()
        {
            CreateMap<Domain.Input.Bid, Domain.Entities.Bid>()
                .ForMember(dest => dest.AuctionAssetId, opt => opt.MapFrom(src => src.AuctionAssetId));
            CreateMap<Domain.Entities.Bid, Domain.DTOs.Bid>();
        }
    }
}
