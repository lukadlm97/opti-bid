using AutoMapper;

namespace OptiBid.Microservices.Auction.Grpc.Profiles
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Domain.DTOs.Customer, CustomersServiceDefinition.CustomerReply>();
            CreateMap<Domain.DTOs.Customer, CustomersServiceDefinition.CustomerDetails>();

            CreateMap<CustomersServiceDefinition.CustomerRequest,Domain.Input.Customer>();
        }
    }
}
