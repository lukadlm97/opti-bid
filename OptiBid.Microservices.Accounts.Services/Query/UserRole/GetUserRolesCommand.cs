using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Accounts.Services.Query.UserRole
{
    public class GetUserRolesCommand : IRequest<IEnumerable<Domain.DTOs.UserRoles>>
    {
    }
}
