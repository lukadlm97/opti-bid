using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace OptiBid.Microservices.Accounts.Services.Profiles
{
    public class UserRoleProfile:Profile
    {
        public UserRoleProfile()
        {
            CreateMap<Domain.Entities.UserRole, Domain.DTOs.UserRoles>();
        }
    }
}
