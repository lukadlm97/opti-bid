using AutoMapper;
using MediatR;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Query.Profession
{
    public class GetProfessionsQueryHandler:IRequestHandler<GetProfessionsCommand,IEnumerable<Domain.DTOs.Profession>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetProfessionsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<Domain.DTOs.Profession>> Handle(GetProfessionsCommand request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Domain.Entities.Profession>,IEnumerable<Domain.DTOs.Profession>>(this._unitOfWork._professionRepository.GetAll().AsEnumerable());
        }
    }
}
