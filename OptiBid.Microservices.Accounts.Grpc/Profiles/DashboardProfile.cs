using AutoMapper;

namespace OptiBid.Microservices.Accounts.Grpc.Profiles
{
    public class DashboardProfile:Profile
    {
        public DashboardProfile()
        {

            CreateMap<Domain.DTOs.ContactType, DashboardServiceDefinition.SingleContactType>();
            CreateMap<Domain.DTOs.Country, DashboardServiceDefinition.SingleCountry>();
            CreateMap<Domain.DTOs.UserRoles, DashboardServiceDefinition.SingleUserRole>();
            CreateMap<Domain.DTOs.Profession, DashboardServiceDefinition.SingleProfession>();


        }
    }
}
