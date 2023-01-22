using AutoMapper;

namespace OptiBid.Microservices.Auction.Grpc.Profiles
{
    public class CategoryProfiles:Profile
    {
        public CategoryProfiles()
        {
            CreateMap<Domain.DTOs.Category, CategoriesServiceDefinition.DataReply>();


        }
    }
}
