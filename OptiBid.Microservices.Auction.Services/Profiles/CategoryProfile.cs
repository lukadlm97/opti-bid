
using AutoMapper;

namespace OptiBid.Microservices.Auction.Services.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Domain.Entities.ProductCategory, Domain.DTOs.Category>();

            CreateMap<Domain.Entities.ServiceCategory, Domain.DTOs.Category>();
        }
    }
}
