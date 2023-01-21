using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Grpc.Profiles.ValueResolvers
{
    public class UserRoleResolver : IValueResolver<UserServiceDefinition.UserRegisterRequest, Domain.Entities.User, UserRole?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public UserRole? Resolve(UserServiceDefinition.UserRegisterRequest source, User destination, UserRole? destMember, ResolutionContext context)
        {
            //TODO: add to appsettings
            var singleRole = _unitOfWork._userRolesRepository.GetAll().FirstOrDefault(x => x.ID == 1);


            return singleRole;
        }
    }
    public class UserRoleIdResolver : IValueResolver<UserServiceDefinition.UserRegisterRequest, Domain.Entities.User, int?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleIdResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int? Resolve(UserServiceDefinition.UserRegisterRequest source, User destination, int? destMember, ResolutionContext context)
        {
            //TODO: add to appsettings
            var singleRole = _unitOfWork._userRolesRepository.GetAll().FirstOrDefault(x => x.ID == 1);


            return singleRole?.ID;
        }
    }
}
