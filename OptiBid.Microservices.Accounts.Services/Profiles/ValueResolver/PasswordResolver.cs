using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Domain.Input;
using OptiBid.Microservices.Accounts.Services.Utility;

namespace OptiBid.Microservices.Accounts.Services.Profiles.ValueResolver
{
    public class PasswordResolver:IValueResolver<Domain.Input.RegisterAccountModel,Domain.Entities.User,string>
    {
        public string Resolve(RegisterAccountModel source, User destination, string destMember, ResolutionContext context)
        {
            return source.Password.GenerateHash();
        }
    }
  
}
