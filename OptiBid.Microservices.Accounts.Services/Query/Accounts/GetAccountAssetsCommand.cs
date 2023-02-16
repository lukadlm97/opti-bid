

using MediatR;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{
    public class GetAccountAssetsCommand:IRequest<(string,string)>
    {
        public string Username { get; set; }
    }
}
