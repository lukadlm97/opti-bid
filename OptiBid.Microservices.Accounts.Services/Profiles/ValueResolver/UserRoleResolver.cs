using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Input;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Services.Profiles.ValueResolver
{
    public class UserRoleResolver : IValueResolver<Domain.Input.RegisterAccountModel, Domain.Entities.User, UserRole?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public UserRole? Resolve(RegisterAccountModel source, User destination, UserRole? destMember, ResolutionContext context)
        {
            //TODO: add to appsettings
            var singleRole = _unitOfWork._userRolesRepository.GetAll().FirstOrDefault(x => x.ID ==1);


            return singleRole;
        }
    }
    public class UserRoleIdResolver : IValueResolver<Domain.Input.RegisterAccountModel, Domain.Entities.User, int?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleIdResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int? Resolve(RegisterAccountModel source, User destination, int? destMember, ResolutionContext context)
        {
            //TODO: add to appsettings
            var singleRole = _unitOfWork._userRolesRepository.GetAll().FirstOrDefault(x => x.ID == 1);


            return singleRole?.ID;
        }
    }

}
