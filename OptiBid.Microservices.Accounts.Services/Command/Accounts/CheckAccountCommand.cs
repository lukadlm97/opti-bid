using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class CheckAccountCommand : IRequest<(bool,Domain.DTOs.UserDetails)>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
