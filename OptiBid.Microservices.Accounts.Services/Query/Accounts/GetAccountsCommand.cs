using MediatR;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{
    public class GetAccountsCommand : IRequest<IEnumerable<User>>
    {
    }
}
