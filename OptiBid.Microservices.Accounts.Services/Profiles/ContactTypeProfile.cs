using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace OptiBid.Microservices.Accounts.Services.Profiles
{
    public class ContactTypeProfile:Profile
    {
        public ContactTypeProfile()
        {
            CreateMap<Domain.Entities.ContactType, Domain.DTOs.ContactType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
          //  CreateMap<IEnumerable<Domain.Entities.ContactType>, IEnumerable<Domain.DTOs.ContactType>>();
        }



    }
}
