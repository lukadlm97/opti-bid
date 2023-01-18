using AutoMapper;
using MediatR;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.Command.Accounts;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsCommand, IEnumerable<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> Handle(GetAccountsCommand request, CancellationToken cancellationToken)
        {
            return  _unitOfWork._usersRepository.GetAll();
        }
    }
}
