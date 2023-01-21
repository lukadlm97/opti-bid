using AutoMapper;

namespace OptiBid.Microservices.Accounts.Grpc.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Domain.DTOs.User, UserServiceDefinition.SingleUser>();
            CreateMap<Domain.DTOs.Skill, UserServiceDefinition.SingleSkill>();
            CreateMap<Domain.DTOs.Contact, UserServiceDefinition.SingleContact>();
            CreateMap<Domain.DTOs.UserDetails, UserServiceDefinition.SingleUserDetails>();


            CreateMap<UserServiceDefinition.ContactRequest, Domain.Entities.Contact>();
            CreateMap<UserServiceDefinition.SkillRequest, Domain.Entities.Skill>();
            CreateMap<UserServiceDefinition.UserRegisterRequest, Domain.Entities.User>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(opt => opt.PasswordHash, opt => opt.MapFrom<Grpc.Profiles.ValueResolvers.PasswordResolver>())
                .ForMember(dest => dest.CountryID, opt => opt.MapFrom<ValueResolvers.CountryIdResolver>())
                .ForMember(dest => dest.Country, opt => opt.MapFrom<ValueResolvers.CountryResolver>())
                .ForMember(dest => dest.Skills, opt => opt.MapFrom<ValueResolvers.SkillsResolver>())
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom<ValueResolvers.ContactsResolver>())
                .ForMember(dest => dest.UserRoleID, opt => opt.MapFrom<ValueResolvers.UserRoleIdResolver>())
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom<ValueResolvers.UserRoleResolver>())
                ; ;

        }
    }
}
