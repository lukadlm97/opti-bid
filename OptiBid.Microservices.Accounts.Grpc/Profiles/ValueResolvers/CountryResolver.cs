using AutoMapper;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Grpc.Profiles.ValueResolvers
{
    public class CountryIdResolver : IValueResolver<UserServiceDefinition.UserRegisterRequest, Domain.Entities.User, int?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryIdResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int? Resolve(UserServiceDefinition.UserRegisterRequest source, User destination, int? destMember, ResolutionContext context)
        {
            var singleCountry = _unitOfWork._countryRepository.GetAll().FirstOrDefault(x => x.ID == source.CountryId);


            return singleCountry?.ID;
        }
    }
    public class CountryResolver : IValueResolver<UserServiceDefinition.UserRegisterRequest, Domain.Entities.User, Domain.Entities.Country?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Country? Resolve(UserServiceDefinition.UserRegisterRequest source, User destination, Country? destMember, ResolutionContext context)
        {
            var singleCountry = _unitOfWork._countryRepository.GetAll().FirstOrDefault(x => x.ID == source.CountryId);


            return singleCountry;
        }
    }
}
