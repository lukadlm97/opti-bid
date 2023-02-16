using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class CompletedFirstLogInAccountCommand : IRequest<bool>
    {
        public string Username { get; set; }
    }
}
