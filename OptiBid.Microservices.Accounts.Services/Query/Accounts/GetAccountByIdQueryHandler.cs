using MediatR;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.UnitOfWork;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdCommand, Domain.Entities.User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountByIdQueryHandler(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Handle(GetAccountByIdCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork._usersRepository.GetById(request.Id, cancellationToken);
        }
    }
}
