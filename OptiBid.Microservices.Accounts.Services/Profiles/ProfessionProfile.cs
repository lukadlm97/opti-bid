using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace OptiBid.Microservices.Accounts.Services.Profiles
{
    public class ProfessionProfile:Profile
    {
        public ProfessionProfile()
        {
            CreateMap<Domain.Entities.Profession, Domain.DTOs.Profession>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
