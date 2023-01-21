using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace OptiBid.Microservices.Accounts.Services.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Domain.Input.RegisterAccountModel, Domain.Entities.User>()
                .ForMember(dest=>dest.Username,
                    opt=>opt.MapFrom(src=>src.Email))
                .ForMember(opt=>opt.PasswordHash,opt=>opt.MapFrom<ValueResolver.PasswordResolver>())
                .ForMember(dest=>dest.CountryID,opt=>opt.MapFrom<ValueResolver.CountryIdResolver>())
                .ForMember(dest=>dest.Country,opt=>opt.MapFrom<ValueResolver.CountryResolver>())
                .ForMember(dest=>dest.Skills,opt=>opt.MapFrom<ValueResolver.SkillsResolver>())
                .ForMember(dest=>dest.Contacts,opt=>opt.MapFrom<ValueResolver.ContactsResolver>())
                .ForMember(dest=>dest.UserRoleID,opt=>opt.MapFrom<ValueResolver.UserRoleIdResolver>())
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom<ValueResolver.UserRoleResolver>())
                ;


            CreateMap<Domain.Entities.User, Domain.DTOs.User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest=>dest.Role,opt=>opt.MapFrom(src=>src.UserRole.Name))
                ;

            CreateMap<Domain.Input.SkillModel, Domain.Entities.Skill>()
                .ForMember(dest => dest.ProfessionID,
                    opt => opt.MapFrom(src => src.ProfessionId))
                .ForMember(dest=>dest.ID,
                    opt=>opt.MapFrom(src=>src.SkillId));

            CreateMap<Domain.Input.ContactModel,Domain.Entities.Contact>()
                .ForMember(dest => dest.ContactTypeID,
                    opt => opt.MapFrom(src => src.ContactTypeId))
                .ForMember(dest => dest.ID,
                    opt => opt.MapFrom(src => src.ContactId));

            CreateMap<Domain.Entities.User, Domain.DTOs.UserDetails>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Contacts,
                    opt => opt.MapFrom(
                        src => src.Contacts.Select(x => new Domain.DTOs.Contact()
                            { Id = x.ID, ContactTypeId = x.ContactTypeID,Content = x.Content,IsActive = x.IsActive})))
                .ForMember(dest=>dest.Skills,
                    opt=>opt.MapFrom(src=>src.Skills.Select(x=>new Domain.DTOs.Skill()
                        {ProfessionId = x.ProfessionID,Id = x.ID,IsActive = x.IsActive})));

        }
    }
}
