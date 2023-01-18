using AutoMapper;
using MediatR;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Query.Country
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesCommand, IEnumerable<Domain.DTOs.Country>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetCountriesQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Domain.DTOs.Country>> Handle(GetCountriesCommand request, CancellationToken cancellationToken)
        {
            return _mapper.Map< IEnumerable < Domain.Entities.Country >,IEnumerable <Domain.DTOs.Country>>(_unitOfWork._countryRepository.GetAll().AsEnumerable());
        }
    }
}
