using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Domain.Input;
using OptiBid.Microservices.Accounts.Services.Utility;

namespace OptiBid.Microservices.Accounts.Grpc.Profiles.ValueResolvers
{
    public class PasswordResolver : IValueResolver<UserServiceDefinition.UserRegisterRequest, Domain.Entities.User, string>
    {
        public string Resolve(UserServiceDefinition.UserRegisterRequest source, User destination, string destMember, ResolutionContext context)
        {
            return source.Password.GenerateHash();
        }
    }
}
