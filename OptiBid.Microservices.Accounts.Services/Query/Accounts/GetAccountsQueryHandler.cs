using MediatR;
using OptiBid.Microservices.Accounts.Data.Repository;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsCommand, List<User>>
    {
        private readonly IRepository<User> _userRepository;

        public GetAccountsQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Handle(GetAccountsCommand request, CancellationToken cancellationToken)
        {
            return _userRepository.GetAll().ToList();
        }
    }
}
