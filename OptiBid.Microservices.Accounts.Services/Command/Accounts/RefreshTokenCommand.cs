using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class RefreshTokenCommand:IRequest<(bool,string)>
    {
        public string Username { get; set; }
        public string RefreshToken { get; set; }
    }
}
