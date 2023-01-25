using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace OptiBid.Microservices.Auction.Services.Profiles
{
    public class AuctionAssetProfile:Profile
    {
        public AuctionAssetProfile()
        {
            CreateMap<Domain.Entities.AuctionAsset, Domain.DTOs.AuctionAsset>();
            CreateMap<Domain.Input.AuctionAsset, Domain.Entities.Product>()
                .ForMember(dest=>dest.CustomerID,opt=>opt.MapFrom(src=>src.CustomerId));
            CreateMap<Domain.Input.AuctionAsset, Domain.Entities.Service>()
                .ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => src.CustomerId));

        }
    }
}
