using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class CreateAccountCommand : IRequest<Domain.DTOs.User>
    {
        public User User { get; set; }
    }
}
