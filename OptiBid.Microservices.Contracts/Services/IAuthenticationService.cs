using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Contracts.Domain.Output;

namespace OptiBid.Microservices.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<OperationResult<string>> SignIn(string username, string password,CancellationToken cancellationToken=default);
        Task<OperationResult<string>> Register(Domain.Input.UserRequest request,CancellationToken cancellationToken=default);
        Task<OperationResult<string>> RenewToken(string username,string refreshToken, CancellationToken cancellationToken = default);
        Task<OperationResult<string>> Verify(string username, string code, CancellationToken cancellationToken);
        Task<OperationResult<string>> Validate(string username, string code, CancellationToken cancellationToken);
    }
}
