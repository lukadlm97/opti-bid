using MediatR;
using OptiBid.Microservices.Accounts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Services.Query.ContactType
{
    public class GetContactTypeCommand : IRequest<IEnumerable<Domain.DTOs.ContactType>>
    {
    }
}
