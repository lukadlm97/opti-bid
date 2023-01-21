using AutoMapper;
using MediatR;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdCommand, (bool,Domain.DTOs.UserDetails)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountByIdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(bool, Domain.DTOs.UserDetails)> Handle(GetAccountByIdCommand request, CancellationToken cancellationToken)
        {
            var obj = await _unitOfWork._usersRepository.GetById(request.Id, cancellationToken);
            return obj == null ? 
                (true, null) :
                (false, _mapper.Map<Domain.DTOs.UserDetails>(obj));
        }
    }
}
