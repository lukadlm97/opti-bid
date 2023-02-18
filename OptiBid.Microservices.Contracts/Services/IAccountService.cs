using OptiBid.Microservices.Contracts.Domain.Output.User;
using OptiBid.Microservices.Contracts.Domain.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Contracts.Services
{
    public interface IAccountService
    {
        Task<OperationResult<UserResult>> GetDetails(string username, CancellationToken cancellationToken);
    }
}
