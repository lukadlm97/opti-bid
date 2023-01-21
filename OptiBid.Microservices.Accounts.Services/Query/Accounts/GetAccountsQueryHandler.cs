using AutoMapper;
using MediatR;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.Command.Accounts;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsCommand, IEnumerable<Domain.DTOs.User>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Domain.DTOs.User>> Handle(GetAccountsCommand request, CancellationToken cancellationToken)
        {
            return  _mapper.Map<IEnumerable<Domain.DTOs.User>>(_unitOfWork._usersRepository.GetAll());
        }
    }
}
