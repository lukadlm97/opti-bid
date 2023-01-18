using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace OptiBid.Microservices.Accounts.Services.Profiles
{
    public class CountryProfile:Profile
    {
        public CountryProfile()
        {
            CreateMap<Domain.Entities.Country, Domain.DTOs.Country>()
                .ForMember(dest => dest.Name, 
                    opt => opt.MapFrom(src => src.NiceName));
        }
    }
}
