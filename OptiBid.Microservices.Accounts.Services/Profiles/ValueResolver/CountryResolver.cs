using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Domain.Input;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Profiles.ValueResolver
{
    public class CountryIdResolver:IValueResolver<Domain.Input.RegisterAccountModel,Domain.Entities.User,int?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryIdResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int? Resolve(RegisterAccountModel source, User destination, int? destMember, ResolutionContext context)
        {
            var singleCountry=  _unitOfWork._countryRepository.GetAll().FirstOrDefault(x => x.ID == source.CountryId);
           

            return singleCountry?.ID;
        }
    }
    public class CountryResolver : IValueResolver<Domain.Input.RegisterAccountModel, Domain.Entities.User, Domain.Entities.Country?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Country? Resolve(RegisterAccountModel source, User destination, Country? destMember, ResolutionContext context)
        {
            var singleCountry = _unitOfWork._countryRepository.GetAll().FirstOrDefault(x => x.ID == source.CountryId);
        

            return singleCountry;
        }
    }
}
