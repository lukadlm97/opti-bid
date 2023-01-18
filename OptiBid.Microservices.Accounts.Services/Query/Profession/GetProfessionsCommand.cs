using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OptiBid.Microservices.Accounts.Services.Query.Profession
{
    public class GetProfessionsCommand: IRequest<IEnumerable<Domain.DTOs.Profession>>
    {
    }
}
