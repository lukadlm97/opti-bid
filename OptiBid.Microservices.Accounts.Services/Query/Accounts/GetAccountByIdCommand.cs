using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Services.Query.Accounts
{
    public class GetAccountByIdCommand : IRequest<Domain.Entities.User>
    {
        public int Id { get; set; }
    }
}
