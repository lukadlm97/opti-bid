
using AutoMapper;

namespace OptiBid.Microservices.Auction.Services.Profiles
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Domain.Entities.Customer, Domain.DTOs.Customer>().ForMember(dest=>dest.DateOpened,opt=>opt.MapFrom(src=>src.DateOpened.ToString()));

            CreateMap<Domain.Input.Customer, Domain.Entities.Customer>()
                .ForMember(dest=>dest.DateOpened,opt=>opt.MapFrom(src=>DateTime.Now.ToUniversalTime()));
        }

    }
}
