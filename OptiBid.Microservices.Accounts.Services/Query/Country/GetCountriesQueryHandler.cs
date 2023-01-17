using MediatR;
using OptiBid.Microservices.Accounts.Data.Repository;

namespace OptiBid.Microservices.Accounts.Services.Query.Country
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesCommand, List<Domain.Entities.Country>>
    {
        private readonly IRepository<Domain.Entities.Country> _countryRepository;

        public GetCountriesQueryHandler(IRepository<Domain.Entities.Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<List<Domain.Entities.Country>> Handle(GetCountriesCommand request, CancellationToken cancellationToken)
        {
            return _countryRepository.GetAll().ToList();
        }
    }
}
