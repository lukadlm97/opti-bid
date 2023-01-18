using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OptiBid.Microservices.Accounts.Services.Query.Country
{
    public class GetCountriesCommand : IRequest<IEnumerable<Domain.DTOs.Country>>
    {

    }
}
