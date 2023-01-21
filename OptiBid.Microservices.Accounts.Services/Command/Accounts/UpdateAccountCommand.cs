using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Accounts.Domain.Entities;

namespace OptiBid.Microservices.Accounts.Services.Command.Accounts
{
    public class UpdateAccountCommand : IRequest<Domain.DTOs.User?>
    {
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
