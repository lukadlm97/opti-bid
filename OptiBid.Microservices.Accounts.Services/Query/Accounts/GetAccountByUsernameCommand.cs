
using MediatR;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{
    public class GetAccountByUsernameCommand:IRequest<(bool, Domain.DTOs.UserDetails)>
    {
        public string Username { get; set; }
    }
}
